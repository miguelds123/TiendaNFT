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
    public class RepositoryTipoTarjeta : IRepositoryTipoTarjeta
    {
        private readonly ProyectoNFTContext _context;

        public RepositoryTipoTarjeta(ProyectoNFTContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(TipoTarjeta entity)
        {
            await _context.Set<TipoTarjeta>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.IdTipoTarjeta;

        }

        public async Task DeleteAsync(int id)
        {
            // Raw Query
            //https://www.learnentityframeworkcore.com/raw-sql/execute-sql
            int rowAffected = _context.Database.ExecuteSql($"Delete TipoTarjeta Where IdTipoTarjeta = {id}");
            await Task.FromResult(1);

        }

        public async Task<ICollection<TipoTarjeta>> FindByDescriptionAsync(string description)
        {
            var collection = await _context
                                     .Set<TipoTarjeta>()
                                     .Where(p => p.Descrpcion.Contains(description))
                                     .ToListAsync();
            return collection;

        }

        public async Task<TipoTarjeta> FindByIdAsync(int id)
        {
            var @object = await _context.Set<TipoTarjeta>().FindAsync(id);
            return @object!;

        }

        public async Task<ICollection<TipoTarjeta>> ListAsync()
        {
            var collection = await _context.Set<TipoTarjeta>().AsNoTracking().ToListAsync();
            return collection;

        }

        public async Task UpdateAsync(int id, TipoTarjeta entity)
        {
            var @object = await FindByIdAsync(id);
            @object.Descrpcion = entity.Descrpcion;
            await _context.SaveChangesAsync();

        }
    }
}
