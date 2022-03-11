using AutoMapper;
using PatPortal.Identity.Application.DTOs.Request;
using PatPortal.Identity.Domain.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Identity.Application.Mapper
{
    public class DtoMapperProfile : Profile
    {
        public DtoMapperProfile()
        {
            CreateMap<UserLoginDto, UserLogin>();
        }
    }
}
