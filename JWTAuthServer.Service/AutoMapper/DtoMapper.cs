using AutoMapper;
using JWTAuthServer.Core.DTOs;
using JWTAuthServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthServer.Service.AutoMapper
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<UserDto, UserApp>().ReverseMap();
        }
    }
}
