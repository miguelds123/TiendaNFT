using AutoMapper;
using MVCProyectoNFT.Application.DTOs;
using MVCProyectoNFT.Application.Services.Interfaces;
using MVCProyectoNFT.Infraestructure.Models;
using MVCProyectoNFT.Infraestructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.Services.Implementations
{
    public class ServiceTipoTarjeta : IServiceTipoTarjeta
    {
        private readonly IRepositoryTipoTarjeta _repository;
        private readonly IMapper _mapper;

        public ServiceTipoTarjeta(IRepositoryTipoTarjeta repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(TipoTarjetaDTO dto)
        {
            // Map TipoTarjetaDTO to TipoTarjeta
            var objectMapped = _mapper.Map<TipoTarjeta>(dto);

            // Return
            return await _repository.AddAsync(objectMapped);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ICollection<TipoTarjetaDTO>> FindByDescriptionAsync(string description)
        {
            var list = await _repository.FindByDescriptionAsync(description);
            var collection = _mapper.Map<ICollection<TipoTarjetaDTO>>(list);
            return collection;

        }

        public async Task<TipoTarjetaDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<TipoTarjetaDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<TipoTarjetaDTO>> ListAsync()
        {
            // Get data from Repository
            var list = await _repository.ListAsync();
            // Map List<TipoTarjeta> to ICollection<TipoTarjetaDTO>
            var collection = _mapper.Map<ICollection<TipoTarjetaDTO>>(list);
            // Return Data
            return collection;
        }

        public async Task UpdateAsync(int id, TipoTarjetaDTO dto)
        {
            var objectMapped = _mapper.Map<TipoTarjeta>(dto);
            await _repository.UpdateAsync(id, objectMapped);
        }

    }
}
