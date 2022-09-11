using Application.Features.Developers.Commands.RegisterDeveloper;
using Application.Features.Developers.Dtos;
using AutoMapper;
using Core.Security.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Developer, RegisteredUserDto>().ReverseMap();
            CreateMap<Developer, RegisterDeveloperCommand>().ReverseMap();
            CreateMap<Developer, UserForRegisterDto>().ReverseMap();
        }
    }
}
