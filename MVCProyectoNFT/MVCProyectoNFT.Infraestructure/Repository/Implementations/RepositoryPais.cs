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
    public class RepositoryPais : IRepositoryPais
    {
        private readonly ProyectoNFTContext _context;

        public RepositoryPais(ProyectoNFTContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Pais entity)
        {
            await _context.Set<Pais>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;

        }

        public async Task DeleteAsync(int id)
        {
            // Raw Query
            //https://www.learnentityframeworkcore.com/raw-sql/execute-sql
            int rowAffected = _context.Database.ExecuteSql($"Delete Pais Where Id = {id}");
            await Task.FromResult(1);

        }

        public async Task<ICollection<Pais>> FindByDescriptionAsync(string description)
        {
            var collection = await _context
                                     .Set<Pais>()
                                     .Where(p => p.Descripcion.Contains(description))
                                     .ToListAsync();
            return collection;

        }

        public async Task<Pais> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Pais>().FindAsync(id);
            return @object!;

        }

        public async Task<ICollection<Pais>> ListAsync()
        {
            var collection = await _context.Set<Pais>().AsNoTracking().ToListAsync();
            return collection;

        }

        public async Task UpdateAsync(int id, Pais entity)
        {
            var @object = await FindByIdAsync(id);
            @object.Descripcion = entity.Descripcion;
            await _context.SaveChangesAsync();

        }
    }
}
