using MVCProyectoNFT.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Infraestructure.Repository.Interface
{
    public interface IRepositoryNFT
    {
        Task<ICollection<Nft>> FindByDescriptionAsync(string description);
        Task<ICollection<Nft>> ListAsync();
        Task<Nft> FindByIdAsync(string id);
        Task<string> AddAsync(Nft entity);
        Task DeleteAsync(string id);
        Task UpdateAsync(string id, Nft entity);
    }
}
