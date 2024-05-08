using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MVCProyectoNFT.Application.Config;
using MVCProyectoNFT.Application.DTOs;
using MVCProyectoNFT.Application.Services.Interfaces;
using MVCProyectoNFT.Infraestructure.Models;
using MVCProyectoNFT.Infraestructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace MVCProyectoNFT.Application.Services.Implementations
{
    public class ServiceFactura : IServiceFactura
    {
        private readonly IRepositoryFactura _repositoryFactura;
        private readonly IRepositoryCliente _repositoryCliente;
        private readonly IRepositoryNFT _repositoryProducto;
        private readonly IMapper _mapper;
        private readonly IOptions<AppConfig> _options;
        private readonly ILogger<ServiceFactura> _logger;

        public ServiceFactura(IRepositoryFactura repositoryFactura,
                              IRepositoryCliente repositoryCliente,
                              IRepositoryNFT repositoryProducto,
                              IMapper mapper,
                              IOptions<AppConfig> options,
                              ILogger<ServiceFactura> logger)
        {
            _repositoryFactura = repositoryFactura;
            _repositoryCliente = repositoryCliente;
            _repositoryProducto = repositoryProducto;
            _mapper = mapper;
            _options = options;
            _logger = logger;
        }

        public async Task<int> AddAsync(FacturaEncabezadoDTO dto)
        {
            var @object = _mapper.Map<FacturaEncabezado>(dto);
            // Find Customer
            var cliente = await _repositoryCliente.FindByIdAsync(dto.IdCliente);
            // Save Bill
            dto.Id = await _repositoryFactura.AddAsync(@object);

            // Create PDF Array
            var pdfBytes = await CreatePDFBill(dto.Id);

            // Directory exist?        
            if (!Directory.Exists("c:\temp"))
                Directory.CreateDirectory(@"C:\temp");
            // Save it locally
            await File.WriteAllBytesAsync(@"c:\temp\" + dto.Id.ToString().Trim() + ".pdf", pdfBytes);

            // Send email with PDF as Attachment
            await SendEmail(cliente!.Correo!, pdfBytes);
            return dto.Id;
        }

        public async Task<int> GetNextReceiptNumber()
        {
            int nextReceipt = await _repositoryFactura.GetNextReceiptNumber();
            return nextReceipt + 1;
        }

        /// <summary>
        /// Send email 
        /// </summary>
        /// <param name="email"></param>
        private async Task<bool> SendEmail(string email, byte[] pdf)
        {
            if (string.IsNullOrEmpty(_options.Value.SmtpConfiguration.Server) || string.IsNullOrEmpty(_options.Value.SmtpConfiguration.PortNumber.ToString()))
            {
                _logger.LogError($"No se encuentra configurado ningun valor para SMPT en {MethodBase.GetCurrentMethod()!.DeclaringType!.FullName}");
                return false;
            }
            if (string.IsNullOrEmpty(_options.Value.SmtpConfiguration.UserName) || string.IsNullOrEmpty(_options.Value.SmtpConfiguration.FromName))
            {
                _logger.LogError($"No se encuentra configurado UserName o FromName en appSettings.json (Dev | Prod) {MethodBase.GetCurrentMethod()!.DeclaringType!.FullName}");
                return false;
            }
            var mailMessage = new MailMessage(
                    new MailAddress(_options.Value.SmtpConfiguration.UserName, _options.Value.SmtpConfiguration.FromName),
                    new MailAddress(email))
            {
                Subject = "Factura Electrónica para " + email,
                Body = "Adjunto Factura Electronica Empresa  NFT K&M S.A.",
                IsBodyHtml = true
            };
            // attach it as a File
            //Attachment attachment = new Attachment(@"c:\\temp\\factura.pdf");
            // Bytes 
            Attachment attachment = new Attachment(new MemoryStream(pdf), "factura.pdf");
            mailMessage.Attachments.Add(attachment);

            using var smtpClient = new SmtpClient(_options.Value.SmtpConfiguration.Server,
                                                  _options.Value.SmtpConfiguration.PortNumber)
            {
                Credentials = new NetworkCredential(_options.Value.SmtpConfiguration.UserName,
                                                    _options.Value.SmtpConfiguration.Password),
                EnableSsl = _options.Value.SmtpConfiguration.EnableSsl,
            };
            await smtpClient.SendMailAsync(mailMessage);
            return true;

        }

        private async Task<byte[]> CreatePDFBill(int id)
        {
            var factura = await _repositoryFactura.FindByIdAsync(id);

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
                        col1.Item().AlignLeft().Text($"Factura : {factura.Id}").FontSize(12);
                        col1.Item().AlignLeft().Text($"Cliente : {factura.IdClienteNavigation.Cedula}- {factura.IdClienteNavigation.Nombre} {factura.IdClienteNavigation.Apellido1}").FontSize(12);
                        col1.Item().AlignLeft().Text($"Fecha   : {factura.Fecha}").FontSize(12);
                        col1.Item().LineHorizontal(0.5f);
                        col1.Item().Text("");
                        col1.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(10);
                                columns.RelativeColumn(10);
                                columns.RelativeColumn(10);
                                
                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#4666FF")
                                .Padding(2).AlignCenter().Text("No").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Producto").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Precio").FontColor("#fff");

                                header.Cell().Background("#4666FF")
                               .Padding(2).AlignCenter().Text("Imagen").FontColor("#fff");

                            });


                            foreach (var item in factura.FacturaDetalle)
                            {
                                // Column 1
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).AlignCenter().Text(item!.IdDetalle.ToString()).FontSize(10);

                                // Column 2
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                               .Padding(2).AlignCenter().Text(item.IdNftNavigation.Nombre.ToString()).FontSize(10);


                                // Column 3
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                               .Padding(2).AlignCenter().Text(item.IdNftNavigation.Valor.ToString("###,###.00")).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Image(item.IdNftNavigation.Imagen).UseOriginalImage();

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
            }).GeneratePdf();

            return pdfByteArray!;
        }

        public async Task Anular(int id)
        {
            await _repositoryFactura.Anular(id);
        }
    }
}
