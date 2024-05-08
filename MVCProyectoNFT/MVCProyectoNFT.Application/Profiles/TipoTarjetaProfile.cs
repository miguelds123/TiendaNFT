using AutoMapper;
using MVCProyectoNFT.Application.DTOs;
using MVCProyectoNFT.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.Profiles
{
    public class TipoTarjetaProfile : Profile
    {
        public TipoTarjetaProfile()
        {
            // Means: Source   , Destination and Reverse :)  
            CreateMap<TipoTarjetaDTO, TipoTarjeta>().ReverseMap();
        }

    }
}
