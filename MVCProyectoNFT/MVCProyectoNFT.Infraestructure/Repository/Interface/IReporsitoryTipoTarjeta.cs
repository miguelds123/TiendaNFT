using MVCProyectoNFT.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Infraestructure.Repository.Interface
{
    public interface IRepositoryTipoTarjeta
    {
        Task<ICollection<TipoTarjeta>> FindByDescriptionAsync(string description);
        Task<ICollection<TipoTarjeta>> ListAsync();
        Task<TipoTarjeta> FindByIdAsync(int id);
        Task<int> AddAsync(TipoTarjeta entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, TipoTarjeta entity);

    }
}
