using MVCProyectoNFT.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Infraestructure.Repository.Interface
{
    public interface IRepositoryFactura
    {
        Task<int> AddAsync(FacturaEncabezado entity);

        Task<int> GetNextReceiptNumber();

        Task<FacturaEncabezado> FindByIdAsync(int id);

        Task<ICollection<FacturaEncabezado>> ListAsync();

        Task<ICollection<FacturaEncabezado>> BillsByClientIdAsync(int id);

        Task AddClienteNFT(string idNft, int idCliente, DateTime fecha, bool estado, int idFactura, string nombre);

        Task Anular(int id);
    }
}
