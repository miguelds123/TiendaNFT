using MVCProyectoNFT.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Infraestructure.Repository.Interface
{
    public interface IRepositoryCliente
    {
        Task<ICollection<Cliente>> FindByDescriptionAsync(string description);
        Task<ICollection<Cliente>> ListAsync();
        Task<Cliente> FindByIdAsync(int id);
        Task<int> AddAsync(Cliente entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, Cliente entity);

    }
}
