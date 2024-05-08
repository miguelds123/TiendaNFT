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
    public class ServiceNft : IServiceNft
    {
        private readonly IRepositoryNFT _repository;
        private readonly IMapper _mapper;

        public ServiceNft(IRepositoryNFT repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> AddAsync(NftDTO dto)
        {
            // Map PaisDTO to Pais
            var objectMapped = _mapper.Map<Nft>(dto);

            // Return
            return await _repository.AddAsync(objectMapped);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ICollection<NftDTO>> FindByDescriptionAsync(string description)
        {
            var list = await _repository.FindByDescriptionAsync(description);
            var collection = _mapper.Map<ICollection<NftDTO>>(list);
            return collection;
        }

        public async Task<NftDTO> FindByIdAsync(string id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<NftDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<NftDTO>> ListAsync()
        {
            // Get data from Repository
            var list = await _repository.ListAsync();
            // Map List<Pais> to ICollection<PaisDTO>
            var collection = _mapper.Map<ICollection<NftDTO>>(list);
            // Return Data
            return collection;
        }

        public async Task UpdateAsync(string id, NftDTO dto)
        {
            var objectMapped = _mapper.Map<Nft>(dto);
            await _repository.UpdateAsync(id, objectMapped);
        }
    }
}
