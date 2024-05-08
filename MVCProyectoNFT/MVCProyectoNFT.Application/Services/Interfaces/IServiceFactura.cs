using MVCProyectoNFT.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.Services.Interfaces
{
    public interface IServiceFactura
    {
        Task<int> AddAsync(FacturaEncabezadoDTO dto);
        Task<int> GetNextReceiptNumber();

        Task Anular(int id);
    }
}
