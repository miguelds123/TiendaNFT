using MVCProyectoNFT.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.Services.Interfaces
{
    public interface IServiceTipoTarjeta
    {
        Task<ICollection<TipoTarjetaDTO>> FindByDescriptionAsync(string description);
        Task<ICollection<TipoTarjetaDTO>> ListAsync();
        Task<TipoTarjetaDTO> FindByIdAsync(int id);
        Task<int> AddAsync(TipoTarjetaDTO dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, TipoTarjetaDTO dto);

    }
}
