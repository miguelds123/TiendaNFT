﻿
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MVCProyectoNFT.Infraestructure.Repository.Implementations;
using MVCProyectoNFT.Infraestructure.Repository.Interface;
using MVCProyectoNFT.Infraestructure.Data;

/*
 Developed By AMV 10-03-2024 10:48 pm
  
*/
namespace MyApp
{

    public class Principal
    {

        /// <summary>
        /// Taken from https://www.c-sharpcorner.com/article/using-dependency-injection-in-net-console-apps/
        /// How to D.I. using Console
        /// </summary>
        /// <returns></returns>
        private static ServiceProvider CreateServices()
        {

            var serviceProvider = new ServiceCollection()
                   .AddLogging(options =>
                   {
                       options.ClearProviders();
                       options.AddConsole();
                   })
                   // Add D.I.
                   .AddTransient<IRepositoryNFT, RepositoryNFT>()
                   .AddTransient<IRepositoryCliente, RepositoryCliente>()
                   .AddTransient<IRepositoryFactura, RepositoryFactura>()
                   // Add SQLServer Connection
                   .AddDbContext<ProyectoNFTContext>(options =>
                   {
                       options.UseSqlServer("Server=localhost;Database=ProyectoNFT;Integrated Security=false;user id=sa;password=123456;Encrypt=false;");
                       options.EnableSensitiveDataLogging();
                   })
                   .AddTransient<MyApplication>()
                   .BuildServiceProvider();

            return serviceProvider;

        }


        /// <summary>
        /// Main 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Config Services (D.I., DataBases, etc)
            var services = CreateServices();
            MyApplication app = services.GetRequiredService<MyApplication>();

            // Call Reports
            app.ProductReport();

            // app.Bill();

        }

        // Class resposible to create reports
        public class MyApplication
        {
            private readonly ILogger<MyApplication> _logger;
            private readonly IRepositoryNFT _repositoryNft;
            private readonly IRepositoryFactura _repositoryFactura;

            public MyApplication(ILogger<MyApplication> logger,
                                 IRepositoryNFT repositoryNft,
                                 IRepositoryFactura repositoryFactura)
            {
                _logger = logger;
                _repositoryNft = repositoryNft;
                _repositoryFactura = repositoryFactura;
            }


            public void Bill()
            {
                // Not async calling. 
                //var collection = _repositoryNft.ListAsync().GetAwaiter();

                var factura = _repositoryFactura.FindByIdAsync(14).GetAwaiter().GetResult();


                // License config ******  IMPORTANT ******
                QuestPDF.Settings.License = LicenseType.Community;

                Document.Create(document =>
                {
                    document.Page(page =>
                    {

                        page.Size(PageSizes.Letter);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.Margin(30);

                        page.Header().Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignLeft().Text("Electronics S.A. ").Bold().FontSize(14).Bold();
                                col.Item().AlignLeft().Text($"Fecha: {DateTime.Now} ").FontSize(9);
                                col.Item().LineHorizontal(1f);
                            });

                        });


                        page.Content().PaddingVertical(10).Column(col1 =>
                        {
                            col1.Item().AlignLeft().Text($"Factura : {factura.Id}").FontSize(12);
                            col1.Item().AlignLeft().Text($"Cliente : {factura.IdClienteNavigation.Cedula.Trim()}- {factura.IdClienteNavigation.Nombre} {factura.IdClienteNavigation.Apellido1}").FontSize(12);
                            col1.Item().AlignLeft().Text($"Fecha   : {factura.Fecha}").FontSize(12);
                            col1.Item().LineHorizontal(0.5f);
                            col1.Item().Text("");
                            col1.Item().Table(tabla =>
                            {
                                tabla.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn(10);
                                    columns.RelativeColumn(5);
                                    columns.RelativeColumn(10);
                                    columns.RelativeColumn(10);
                                    columns.RelativeColumn(10);
                                });

                                tabla.Header(header =>
                                {
                                    header.Cell().Background("#4666FF")
                                    .Padding(2).AlignCenter().Text("No").FontColor("#fff");

                                    header.Cell().Background("#4666FF")
                                   .Padding(2).AlignCenter().Text("Nft").FontColor("#fff");

                                    header.Cell().Background("#4666FF")
                                   .Padding(2).AlignCenter().Text("Cantidad").FontColor("#fff");

                                    header.Cell().Background("#4666FF")
                                   .Padding(2).AlignCenter().Text("Precio").FontColor("#fff");

                                    header.Cell().Background("#4666FF")
                                   .Padding(2).AlignCenter().Text("Impuesto").FontColor("#fff");

                                    header.Cell().Background("#4666FF")
                                   .Padding(2).AlignCenter().Text("Total").FontColor("#fff");

                                });


                                foreach (var item in factura.FacturaDetalle)
                                {
                                    // Column 1
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                    .Padding(2).AlignCenter().Text(item!.IdDetalle.ToString()).FontSize(10);

                                    // Column 2
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                   .Padding(2).Text(item.IdNftNavigation.Nombre.ToString()).FontSize(10);

                                    // Column 3
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                   .Padding(2).AlignCenter().Text(1.ToString()).FontSize(10);

                                    // Column 4
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                   .Padding(2).AlignRight().Text(item.Precio.Value.ToString("###,###.00")).FontSize(10);

                                    // Column 6
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                   .Padding(2).AlignRight().Text(item.Precio.Value.ToString("###,###.00")).FontSize(10);
                                }

                            });


                            var granTotal = factura.FacturaDetalle.Sum(p => p.Precio);

                            col1.Item().AlignRight().Text("Total " + granTotal.Value.ToString("###,###.00")).FontSize(12).Bold();

                        });


                        page.Footer()
                        .AlignRight()
                        .Text(txt =>
                        {
                            txt.Span("Página ").FontSize(10);
                            txt.CurrentPageNumber().FontSize(10);
                            txt.Span(" de ").FontSize(10);
                            txt.TotalPages().FontSize(10);
                        });
                    });
                }).ShowInPreviewer();
            }

            public void ProductReport()
            {
                // Not async calling. 
                var collection = _repositoryNft.ListAsync().GetAwaiter();

                // License config ******  IMPORTANT ******
                QuestPDF.Settings.License = LicenseType.Community;

                Document.Create(document =>
                {
                    document.Page(page =>
                    {

                        page.Size(PageSizes.Letter);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.Margin(30);

                        page.Header().Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignLeft().Text("NFT K&M S.A. ").Bold().FontSize(14).Bold();
                                col.Item().AlignLeft().Text($"Fecha: {DateTime.Now} ").FontSize(9);
                                col.Item().LineHorizontal(1f);
                            });

                        });


                        page.Content().PaddingVertical(10).Column(col1 =>
                        {
                            col1.Item().AlignCenter().Text("Reporte de Nfts").FontSize(14).Bold();
                            col1.Item().Text("");
                            col1.Item().LineHorizontal(0.5f);

                            col1.Item().Table(tabla =>
                            {
                                tabla.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();

                                });

                                tabla.Header(header =>
                                {
                                    header.Cell().Background("#4666FF")
                                    .Padding(2).AlignCenter().Text("Nft").FontColor("#fff");

                                    header.Cell().Background("#4666FF")
                                   .Padding(2).AlignCenter().Text("Foto").FontColor("#fff");

                                    header.Cell().Background("#4666FF")
                                   .Padding(2).AlignCenter().Text("Cantidad").FontColor("#fff");

                                    header.Cell().Background("#4666FF")
                                   .Padding(2).AlignCenter().Text("Precio").FontColor("#fff");

                                    header.Cell().Background("#4666FF")
                                   .Padding(2).AlignCenter().Text("Total").FontColor("#fff");
                                });

                                foreach (var item in collection.GetResult())
                                {

                                    var total = item.Cantidad * item.Valor;

                                    // Column 1
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                    .Padding(2).Text(item.Id.ToString() + "-" + item.Nombre.PadRight(50, '.').Substring(0, 15)).FontSize(10);

                                    // Column 2
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                    .Padding(2).Image(item.Imagen).UseOriginalImage();

                                    // Column 3
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                        .Padding(2).AlignRight().Text(item.Cantidad.ToString()).FontSize(10);
                                    // Column 4
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                           .Padding(2).AlignRight().Text(item.Valor.ToString("###,###.00")).FontSize(10);
                                    // Column 5
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                         .Padding(2).AlignRight().Text(total.ToString("###,###.00")).FontSize(10);
                                }

                            });

                            var granTotal = collection.GetResult().Sum(p => p.Cantidad * p.Valor);

                            col1.Item().AlignRight().Text("Total " + granTotal.ToString("###,###.00")).FontSize(12).Bold();

                        });


                        page.Footer()
                        .AlignRight()
                        .Text(txt =>
                        {
                            txt.Span("Página ").FontSize(10);
                            txt.CurrentPageNumber().FontSize(10);
                            txt.Span(" de ").FontSize(10);
                            txt.TotalPages().FontSize(10);
                        });
                    });
                }).ShowInPreviewer();
            }
        }
    }
}








