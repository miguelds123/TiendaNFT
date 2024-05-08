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
    public class NftProfile : Profile
    {
        public NftProfile()
        {
            // Means: Source   , Destination and Reverse :)  
            CreateMap<NftDTO, Nft>().ReverseMap();
        }
    }
}
