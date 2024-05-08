using AutoMapper;
using MVCProyectoNFT.Application.DTOs;
using MVCProyectoNFT.Application.Services.Interfaces;
using MVCProyectoNFT.Infraestructure.Repository.Implementations;
using MVCProyectoNFT.Infraestructure.Repository.Interface;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.Services.Implementations
{
    public class ServiceReporte : IServiceReporte
    {

        private readonly IRepositoryNFT _repositoryNft;
        private readonly IRepositoryFactura _repositoryFactura;
        private readonly IServiceClienteNFT _serviceClienteNFT;
        private readonly IRepositoryClienteNFT _repositoryClienteNFT;
        private readonly IRepositoryCliente _repositoryCliente;
        private readonly IMapper _mapper;

        public ServiceReporte(IRepositoryNFT repositoryNft,
                              IRepositoryFactura repositoryFactura,
                              IMapper mapper,
                              IServiceClienteNFT serviceClienteNFT,
                              IRepositoryClienteNFT repositoryClienteNFT,
                              IRepositoryCliente repositoryCliente)
        {
            _repositoryNft = repositoryNft;
            _repositoryFactura = repositoryFactura;
            _mapper = mapper;
            _serviceClienteNFT = serviceClienteNFT;
            _repositoryClienteNFT = repositoryClienteNFT;
            _repositoryCliente = repositoryCliente;
        }

        public Task<ICollection<FacturaEncabezadoDTO>> BillsByClientIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> NFTReport()
        {
            var collection = await _repositoryNft.ListAsync();

            // License config ******  IMPORTANT ******
            QuestPDF.Settings.License = LicenseType.Community;

            // return ByteArrays
            var pdfByteArray = Document.Create(document =>
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
                        col1.Item().AlignCenter().Text("Reporte de NFT").FontSize(14).Bold();
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
                                .Padding(2).AlignCenter().Text("ID NFT").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                                .Padding(2).AlignCenter().Text("Nombre del NFT").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Foto").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Cantidad").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Precio").FontColor("#fff");

                                
                            });

                            foreach (var item in collection)
                            {

                                var total = item.Valor;

                                // Column 1
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).AlignCenter().Text(item.Id.ToString());

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).AlignCenter().Text(item.Nombre).FontSize(10);

                                // Column 3
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Image(item.Imagen).UseOriginalImage();

                                // Column 4
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                    .Padding(2).AlignCenter().Text(1.ToString()).FontSize(10);
                                // Column 5
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                       .Padding(2).AlignCenter().Text(item.Valor.ToString("###,###.00")).FontSize(10);
                                
                            }

                        });

                        var granTotal = collection.Sum(p => p.Valor);

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
            }).GeneratePdf();

            //File.WriteAllBytes(@"C:\temp\ProductReport.pdf", pdfByteArray);
            return pdfByteArray;
        }

        public async Task<byte[]> DuenoNFTReportPDF(string nombre)
        {
            // Get Data
            var collection = await _serviceClienteNFT.FindByNombreNFTAsync(nombre);

            var infoNFT = await _serviceClienteNFT.EnviarNFT(nombre);

            // License config ******  IMPORTANT ******
            QuestPDF.Settings.License = LicenseType.Community;

            // return ByteArrays
            var pdfByteArray = Document.Create(document =>
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
                        col1.Item().AlignCenter().Text("Reporte de Productos").FontSize(14).Bold();
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
                                columns.RelativeColumn();
                                columns.RelativeColumn();

                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#4666FF")
                                .Padding(2).AlignCenter().Text("Cedula").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Nombre Completo").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Correo").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Sexo").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Fecha de Nacimiento").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Pais").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Foto").FontColor("#fff");
                            });

                            // Column 1
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).AlignCenter().Text(collection.Cedula).FontSize(10);

                            // Column 2
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).AlignCenter().Text(collection.Nombre + " " + collection.Apellido1 + " " + collection.Apellido2).FontSize(10);

                            // Column 3
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                .Padding(2).AlignCenter().Text(collection.Correo.ToString()).FontSize(10);
                            // Column 4
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                .Padding(2).AlignCenter().Text(collection.Sexo.ToString()).FontSize(10);
                            // Column 5
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                .Padding(2).AlignCenter().Text(collection.FechaN.ToString()).FontSize(10);

                            // Column 6
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                .Padding(2).AlignCenter().Text(collection.IdPais.ToString()).FontSize(10);

                            // Column 7
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Image(infoNFT.Imagen).UseOriginalImage();

                        });

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
            }).GeneratePdf();

            //File.WriteAllBytes(@"C:\temp\ProductReport.pdf", pdfByteArray);
            return pdfByteArray;
        }

        public async Task<byte[]> ListaVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            var collection = await _repositoryClienteNFT.ListAsync(fechaInicio, fechaFin);

            // License config ******  IMPORTANT ******
            QuestPDF.Settings.License = LicenseType.Community;

            var pdfByteArray = QuestPDF.Fluent.Document.Create(document =>
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
                        col1.Item().AlignCenter().Text("Reporte de Ventas").FontSize(14).Bold();
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
                                columns.RelativeColumn();
                                columns.RelativeColumn();

                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#4666FF")
                                .Padding(2).AlignCenter().Text("ID").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Cedula").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Nombre Cliente").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Fecha").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Número de Tarjeta").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("NFT").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Total").FontColor("#fff");

                            });

                            foreach (var item in collection)
                            {

                                // Column 1
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).AlignCenter().Text(item.IdFactura).FontSize(10);

                                // Column 2
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                    .Padding(2).AlignCenter().Text(item.IdClienteNavigation.Cedula.ToString()).FontSize(10);

                                // Column 3
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).AlignCenter().Text(item.IdClienteNavigation.Nombre + " " + item.IdClienteNavigation.Apellido1 + " " + item.IdClienteNavigation.Apellido2).FontSize(10);

                                // Column 4
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                    .Padding(2).AlignCenter().Text(item.Fecha.ToString()).FontSize(10);

                                // Column 5
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                    .Padding(2).AlignCenter().Text(item.IdFacturaNavigation.NumeroTarjeta.ToString()).FontSize(10);

                                // Column 6
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                    .Padding(2).AlignCenter().Text(item.IdNftNavigation.Nombre.ToString()).FontSize(10);

                                // Column 7
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                    .Padding(2).AlignCenter().Text(item.IdNftNavigation.Valor.ToString()).FontSize(10);

                            }

                        });

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
            }).GeneratePdf();

            return pdfByteArray!;
        }

        public async Task<byte[]> ClienteReport()
        {
            var collection = await _repositoryCliente.ListAsync();

            // License config ******  IMPORTANT ******
            QuestPDF.Settings.License = LicenseType.Community;

            // return ByteArrays
            var pdfByteArray = Document.Create(document =>
            {
                document.Page(page =>
                {

                    page.Size(PageSizes.Letter);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.Margin(60);

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
                        col1.Item().AlignCenter().Text("Reporte de Clientes").FontSize(14).Bold();
                        col1.Item().Text("");
                        col1.Item().LineHorizontal(0.60f);

                        col1.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();

                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#4666FF")
                                .Padding(2).AlignCenter().Text("ID").FontColor("#fff");
                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Cédula").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                                .Padding(2).AlignCenter().Text("Nombre del Cliente").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Correo").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Sexo").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("País").FontColor("#fff");


                            });

                            foreach (var item in collection)
                            {


                                // Column 1
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).AlignCenter().Text(item.Id.ToString());

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                             .Padding(2).AlignCenter().Text(item.Cedula).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).AlignCenter().Text(item.Nombre + " " + item.Apellido1 + " " + item.Apellido2).FontSize(10);



                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                             .Padding(2).AlignCenter().Text(item.Correo).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                             .Padding(2).AlignCenter().Text(item.Sexo).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                          .Padding(2).AlignCenter().Text(item.IdPaisNavigation.Descripcion).FontSize(10);




                            }

                        });



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
            }).GeneratePdf();

            //File.WriteAllBytes(@"C:\temp\ProductReport.pdf", pdfByteArray);
            return pdfByteArray;
        }
    }
}
