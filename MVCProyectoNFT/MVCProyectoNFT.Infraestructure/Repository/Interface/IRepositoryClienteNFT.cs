using MVCProyectoNFT.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Infraestructure.Repository.Interface
{
    public interface IRepositoryClienteNFT
    {
        Task<ICollection<ClienteNft>> FindByNombreNFTAsync(string nombre);

        Task<ICollection<ClienteNft>> ListAsync(DateTime fechaInicio, DateTime fechaFin);
    }
}
