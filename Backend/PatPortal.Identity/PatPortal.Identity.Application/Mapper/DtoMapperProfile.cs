using AutoMapper;
using PatPortal.Identity.Application.DTOs.Request;
using PatPortal.Identity.Application.DTOs.Response;
using PatPortal.Identity.Domain.Entities;
using PatPortal.Identity.Domain.Entities.Request;
using PatPortal.Identity.Domain.Entities.Response;
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
            CreateMap<UserCredentials, UserCredentialsDto>();
            CreateMap<User, UserForViewDto>();
            CreateMap<UserForCreationDto, UserCreate>();
            CreateMap<UserForCreationDto, UserCreate>()
                .ForMember(dest => dest.GlobalId, guid => guid.MapFrom(src => Guid.Parse(src.GlobalId)));
        }
    }
}
