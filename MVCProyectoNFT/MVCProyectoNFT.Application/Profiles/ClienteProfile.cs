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
    public class ClienteProfile : Profile
    {

        public ClienteProfile()
        {
            // Means: Source   , Destination and Reverse :)  
            CreateMap<ClienteDTO, Cliente>().ReverseMap();

            CreateMap<Cliente, ClienteDTO>()
                .ForMember(dest => dest.Nombre, orig => orig.MapFrom(x => x.Nombre))
                .ForMember(dest => dest.Apellido1, orig => orig.MapFrom(x => x.Apellido1))
                .ForMember(dest => dest.Apellido2, orig => orig.MapFrom(x => x.Apellido2))
                .ForMember(dest => dest.Correo, orig => orig.MapFrom(x => x.Correo))
                .ForMember(dest => dest.Sexo, orig => orig.MapFrom(x => x.Sexo))
                .ForMember(dest => dest.FechaN, orig => orig.MapFrom(x => x.FechaN))
                .ForMember(dest => dest.Estado, orig => orig.MapFrom(x => x.Estado))
                .ForMember(dest => dest.IdPais, orig => orig.MapFrom(x => x.IdPais))
                .ForMember(dest => dest.Cedula, orig => orig.MapFrom(x => x.Cedula))
                .ForMember(dest => dest.DescripcionPais, orig => orig.MapFrom(x => x.IdPaisNavigation.Descripcion));
        }

    }
}
