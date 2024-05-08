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
    public class FacturaProfile : Profile
    {
        public FacturaProfile()
        {
            CreateMap<FacturaEncabezadoDTO, FacturaEncabezado>().ReverseMap();
            CreateMap<FacturaDetalleDTO, FacturaDetalle>().ReverseMap();

            CreateMap<FacturaEncabezadoDTO, FacturaEncabezado>()
                 .ForMember(dest => dest.Id, orig => orig.MapFrom(x => x.Id))
                 .ForMember(dest => dest.IdTipoTarjeta, orig => orig.MapFrom(x => x.IdTipoTarjeta))
                 .ForMember(dest => dest.IdCliente, orig => orig.MapFrom(x => x.IdCliente))
                 .ForMember(dest => dest.FacturaDetalle, orig => orig.MapFrom(x => x.ListFacturaDetalle)).ReverseMap();

            /*          
            CreateMap<FacturaEncabezado, FacturaEncabezadoDTO>()
                .ForMember(dest => dest.IdFactura, orig => orig.MapFrom(x => x.IdFactura))
                .ForMember(dest => dest.IdTarjeta, orig => orig.MapFrom(x => x.IdTarjeta))
                .ForMember(dest => dest.IdCliente, orig => orig.MapFrom(x => x.IdCliente))
                .ForMember(dest => dest.ListFacturaDetalle, orig => orig.MapFrom(x => x.FacturaDetalle));
            */
        }
    }
}
