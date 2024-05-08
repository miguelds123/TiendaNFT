using MVCProyectoNFT.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.Services.Interfaces
{
    public interface IServiceNft
    {
        Task<ICollection<NftDTO>> FindByDescriptionAsync(string description);
        Task<ICollection<NftDTO>> ListAsync();
        Task<NftDTO> FindByIdAsync(string id);
        Task<string> AddAsync(NftDTO dto);
        Task DeleteAsync(string id);
        Task UpdateAsync(string id, NftDTO dto);
    }
}
