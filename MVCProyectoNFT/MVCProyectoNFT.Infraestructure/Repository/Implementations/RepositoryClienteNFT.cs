using Microsoft.EntityFrameworkCore;
using MVCProyectoNFT.Infraestructure.Data;
using MVCProyectoNFT.Infraestructure.Models;
using MVCProyectoNFT.Infraestructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Infraestructure.Repository.Implementations
{
    public class RepositoryClienteNFT : IRepositoryClienteNFT
    {
        private readonly ProyectoNFTContext _context;

        public RepositoryClienteNFT(ProyectoNFTContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ClienteNft>> FindByNombreNFTAsync(string nombre)
        {
            var collection = await _context
                                     .Set<ClienteNft>()
                                     .Where(p => p.NombreNft.Contains(nombre))
                                     .ToListAsync();
            return collection;

        }

        public async Task<ICollection<ClienteNft>> ListAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var collection = await _context.Set<ClienteNft>().Include(b => b.IdFacturaNavigation).Include(b => b.IdClienteNavigation).Include(b => b.IdNftNavigation).AsNoTracking().ToListAsync();

            var listaDueno = collection.Where(item => item.Estado == true && item.Fecha >= fechaInicio && item.Fecha <= fechaFin)
                                 .ToList();

            return listaDueno!;
        }
    }
}
