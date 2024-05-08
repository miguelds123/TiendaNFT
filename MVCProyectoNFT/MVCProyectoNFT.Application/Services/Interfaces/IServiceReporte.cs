using MVCProyectoNFT.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.Services.Interfaces
{
    public interface IServiceReporte
    {
        Task<byte[]> NFTReport();

        Task<ICollection<FacturaEncabezadoDTO>> BillsByClientIdAsync(string id);

        Task<byte[]> DuenoNFTReportPDF(string nombre);

        Task<byte[]> ListaVentas(DateTime fechaInicio, DateTime fechaFin);

        Task<byte[]> ClienteReport();
    }
}
