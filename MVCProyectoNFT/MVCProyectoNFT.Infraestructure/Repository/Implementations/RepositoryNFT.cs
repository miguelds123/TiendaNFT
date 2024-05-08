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
    public class RepositoryNFT : IRepositoryNFT
    {
        private readonly ProyectoNFTContext _context;

        public RepositoryNFT(ProyectoNFTContext context)
        {
            _context = context;
        }
        public async Task<string> AddAsync(Nft entity)
        {
            await _context.Set<Nft>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(string id)
        {
            // Raw Query
            //https://www.learnentityframeworkcore.com/raw-sql/execute-sql
            int rowAffected = _context.Database.ExecuteSql($"Delete NFT Where Id = {id}");
            await Task.FromResult(1);
        }

        public async Task<ICollection<Nft>> FindByDescriptionAsync(string description)
        {
            var collection = await _context
                                     .Set<Nft>()
                                     .Where(p => p.Nombre.Contains(description))
                                     .ToListAsync();
            return collection;
        }

        public async Task<Nft> FindByIdAsync(string id)
        {
            var @object = await _context.Set<Nft>().FindAsync(id);
            return @object!;
        }

        public async Task<ICollection<Nft>> ListAsync()
        {
            var collection = await _context.Set<Nft>().AsNoTracking().ToListAsync();
            return collection;
        }

        public async Task UpdateAsync(string id, Nft entity)
        {
            var @object = await FindByIdAsync(id);
            @object.Nombre = entity.Nombre;
            @object.Autor = entity.Autor;
            @object.Valor = entity.Valor;
            @object.Cantidad = entity.Cantidad;
            //@object.IdCliente = entity.IdCliente;
            @object.Imagen = entity.Imagen;
            await _context.SaveChangesAsync();
        }
    }
}
