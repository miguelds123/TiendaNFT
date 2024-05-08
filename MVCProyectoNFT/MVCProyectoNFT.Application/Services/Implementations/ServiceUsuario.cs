using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using MVCProyectoNFT.Application.Config;
using MVCProyectoNFT.Application.DTOs;
using MVCProyectoNFT.Application.Services.Interfaces;
using MVCProyectoNFT.Application.Utils;
using MVCProyectoNFT.Infraestructure.Models;
using MVCProyectoNFT.Infraestructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.Services.Implementations
{
    public class ServiceUsuario : IServiceUsuario
    {

        private readonly IRepositoryUsuario _repository;
        private readonly IMapper _mapper;
        private readonly IOptions<AppConfig> _options;

        public ServiceUsuario(IRepositoryUsuario repository, IMapper mapper, IOptions<AppConfig> options)
        {
            _repository = repository;
            _mapper = mapper;
            _options = options;
        }

        public async Task<string> AddAsync(UsuarioDTO dto)
        {
            // Read secret
            string secret = "0d5e1feb-3583-45d1-8b7c-b60e86805420";
            //  Get Encrypted password
            string passwordEncrypted = Cryptography.Encrypt(dto.Contrasenna!, secret);
            // Set Encrypted password to dto
            dto.Contrasenna = passwordEncrypted;
            var objectMapped = _mapper.Map<Usuario>(dto);
            // Return
            return await _repository.AddAsync(objectMapped);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ICollection<UsuarioDTO>> FindByDescriptionAsync(string description)
        {
            var list = await _repository.FindByDescriptionAsync(description);
            var collection = _mapper.Map<ICollection<UsuarioDTO>>(list);
            return collection;
        }

        public async Task<UsuarioDTO> FindByIdAsync(string id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<UsuarioDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<UsuarioDTO>> ListAsync()
        {
            // Get data from Repository
            var list = await _repository.ListAsync();
            // Map List<*> to ICollection<*>
            var collection = _mapper.Map<ICollection<UsuarioDTO>>(list);
            // Return Data
            return collection;
        }

        public async Task<UsuarioDTO> LoginAsync(string id, string password)
        {
            UsuarioDTO usuarioDTO = null!;

            // Read secret, de momento tuve que ponerlo aqui porque no lo quiere leer desde el appsettings.Development.json
            string secret = "0d5e1feb-3583-45d1-8b7c-b60e86805420";
            //  Get Encrypted password
            string passwordEncrypted = Cryptography.Encrypt(password, secret);

            var @object = await _repository.LoginAsync(id, passwordEncrypted);

            if (@object != null)
            {
                usuarioDTO = _mapper.Map<UsuarioDTO>(@object);
            }

            return usuarioDTO;
        }

        public async Task UpdateAsync(string id, UsuarioDTO dto)
        {
            var @object = await _repository.FindByIdAsync(id);
            //       source, destination
            _mapper.Map(dto, @object!);
            await _repository.UpdateAsync();
        }
    }
}
