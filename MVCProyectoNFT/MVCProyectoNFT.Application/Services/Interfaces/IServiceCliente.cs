using MVCProyectoNFT.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.Services.Interfaces
{
    public interface IServiceCliente
    {
        Task<ICollection<ClienteDTO>> FindByDescriptionAsync(string description);
        Task<ICollection<ClienteDTO>> ListAsync();
        Task<ClienteDTO> FindByIdAsync(int id);
        Task<int> AddAsync(ClienteDTO dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, ClienteDTO dto);

    }
}
