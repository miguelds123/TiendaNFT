using MVCProyectoNFT.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.Services.Interfaces
{
    public interface IServiceClienteNFT
    {
        Task<ClienteDTO> FindByNombreNFTAsync(string nombre);

        Task<NftDTO> EnviarNFT(string nombre);
    }
}
