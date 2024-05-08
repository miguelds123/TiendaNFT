﻿using Microsoft.EntityFrameworkCore;
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
    public class RepositoryUsuario : IRepositoryUsuario
    {
        private readonly ProyectoNFTContext _context;

        public RepositoryUsuario(ProyectoNFTContext context)
        {
            _context = context;
        }

        public async Task<string> AddAsync(Usuario entity)
        {
            await _context.Set<Usuario>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.NombreUsuario;
        }

        public async Task DeleteAsync(string id)
        {
            var @object = await FindByIdAsync(id);
            _context.Remove(@object);
            _context.SaveChanges();
        }

        public async Task<ICollection<Usuario>> FindByDescriptionAsync(string description)
        {
            var collection = await _context
                                     .Set<Usuario>()
                                     .Where(p => p.Nombre.Contains(description))
                                     .ToListAsync();
            return collection;
        }

        public async Task<Usuario> FindByIdAsync(string id)
        {
            var @object = await _context.Set<Usuario>().FindAsync(id);

            return @object!;
        }

        public async Task<ICollection<Usuario>> ListAsync()
        {
            var collection = await _context.Set<Usuario>()
                                       .Include(b => b.IdTipoUsuarioNavigation)
                                       .AsNoTracking().ToListAsync();
            return collection;
        }

        public async Task<Usuario> LoginAsync(string id, string password)
        {
            var @object = await _context.Set<Usuario>()
                                    .Include(b => b.IdTipoUsuarioNavigation)
                                    .Where(p => p.NombreUsuario == id && p.Contrasenna == password)
                                    .FirstOrDefaultAsync();
            return @object!;
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
