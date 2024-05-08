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
    public class PaisProfile : Profile
    {
        public PaisProfile()
        {
            // Means: Source   , Destination and Reverse :)  
            CreateMap<PaisDTO, Pais>().ReverseMap();
        }

    }
}
