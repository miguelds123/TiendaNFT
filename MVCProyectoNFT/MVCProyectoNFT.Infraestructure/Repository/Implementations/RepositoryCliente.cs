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
    public class RepositoryCliente : IRepositoryCliente
    {
        private readonly ProyectoNFTContext _context;

        public RepositoryCliente(ProyectoNFTContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Cliente entity)
        {
            await _context.Set<Cliente>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;

        }

        public async Task DeleteAsync(int id)
        {
            // Raw Query
            //https://www.learnentityframeworkcore.com/raw-sql/execute-sql
            int rowAffected = _context.Database.ExecuteSql($"Delete Cliente Where Id = {id}");
            await Task.FromResult(1);

        }

        public async Task<ICollection<Cliente>> FindByDescriptionAsync(string description)
        {
            var collection = await _context
                                     .Set<Cliente>()
                                     .Where(p => p.Nombre.Contains(description))
                                     .ToListAsync();
            return collection;

        }

        public async Task<Cliente> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Cliente>().FindAsync(id);
            return @object!;

        }

        public async Task<ICollection<Cliente>> ListAsync()
        {
            var collection = await _context.Set<Cliente>().Include(b => b.IdPaisNavigation).AsNoTracking().ToListAsync();
            return collection;

        }

        public async Task UpdateAsync(int id, Cliente entity)
        {
            var @object = await FindByIdAsync(id);
            @object.Id = entity.Id;
            @object.Nombre = entity.Nombre;
            @object.Apellido1 = entity.Apellido1;
            @object.Apellido2 = entity.Apellido2;
            @object.Correo = entity.Correo;
            @object.Sexo = entity.Sexo;
            @object.FechaN = entity.FechaN;
            @object.IdPais = entity.IdPais;
            @object.Estado = entity.Estado;
            await _context.SaveChangesAsync();

        }
    }
}
