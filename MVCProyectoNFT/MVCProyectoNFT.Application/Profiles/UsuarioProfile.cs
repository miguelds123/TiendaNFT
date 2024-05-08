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
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {

            // Means: Source   , Destination and Reverse :)  
            CreateMap<UsuarioDTO, Usuario>();

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.NombreUsuario, orig => orig.MapFrom(x => x.NombreUsuario))
                .ForMember(dest => dest.Nombre, orig => orig.MapFrom(x => x.Nombre))
                .ForMember(dest => dest.Apellido1, orig => orig.MapFrom(x => x.Apellido1))
                .ForMember(dest => dest.Apellido2, orig => orig.MapFrom(x => x.Apellido2))
                .ForMember(dest => dest.Estado, orig => orig.MapFrom(x => x.Estado))
                .ForMember(dest => dest.IdTipoUsuario, orig => orig.MapFrom(x => x.IdTipoUsuario))
                .ForMember(dest => dest.DescripcionRol, orig => orig.MapFrom(x => x.IdTipoUsuarioNavigation.Descripcion));
        }
    }
}
