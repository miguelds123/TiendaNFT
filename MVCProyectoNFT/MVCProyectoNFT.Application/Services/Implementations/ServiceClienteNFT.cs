using AutoMapper;
using MVCProyectoNFT.Application.DTOs;
using MVCProyectoNFT.Application.Services.Interfaces;
using MVCProyectoNFT.Infraestructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVCProyectoNFT.Application.Services.Implementations
{
    public class ServiceClienteNFT : IServiceClienteNFT
    {
        private readonly IRepositoryCliente _repositoryCliente;
        private readonly IRepositoryClienteNFT _repositoryClienteNFT;
        private readonly IRepositoryNFT _repositoryNFT;
        private readonly IMapper _mapper;

        public ServiceClienteNFT(IRepositoryCliente repositoryCliente, IMapper mapper, IRepositoryClienteNFT repositoryClienteNFT, IRepositoryNFT repositoryNFT)
        {
            _repositoryCliente = repositoryCliente;
            _repositoryClienteNFT = repositoryClienteNFT;
            _mapper = mapper;
            _repositoryNFT = repositoryNFT;
        }

        public async Task<ClienteDTO> FindByNombreNFTAsync(string nombre)
        {
            var list = await _repositoryClienteNFT.FindByNombreNFTAsync(nombre);

            if (list == null || !list.Any())
            {
                return null;
            }

            // Filtrar los elementos con Estado verdadero y obtener el mayor Id
            var ultimoId = list.Where(item => item.Estado == true)
                               .Max(item => item.Id);

            // Crear una lista para almacenar los elementos con Estado verdadero y con el mayor Id
            var listaDueno = list.Where(item => item.Estado == true && item.Id == ultimoId)
                                 .ToList();



            var dueno = listaDueno[0];

            var infoDueno = await _repositoryCliente.FindByIdAsync(dueno.IdCliente);

            var nft = await _repositoryNFT.FindByIdAsync(dueno.IdNft);

            var collection = _mapper.Map<ClienteDTO>(infoDueno);

            return collection;

        }

        public async Task<NftDTO> EnviarNFT(string nombre)
        {
            var list = await _repositoryClienteNFT.FindByNombreNFTAsync(nombre);

            // Filtrar los elementos con Estado verdadero y obtener el mayor Id
            var ultimoId = list.Where(item => item.Estado == true)
                               .Max(item => item.Id);

            // Crear una lista para almacenar los elementos con Estado verdadero y con el mayor Id
            var listaDueno = list.Where(item => item.Estado == true && item.Id == ultimoId)
                                 .ToList();



            var dueno = listaDueno[0];

            var nft = await _repositoryNFT.FindByIdAsync(dueno.IdNft);

            var collection = _mapper.Map<NftDTO>(nft);

            return collection;

        }
    }
}
