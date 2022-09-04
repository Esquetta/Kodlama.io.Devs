using Application.Features.Languages.Commands.CreateLanguage;
using Application.Features.Languages.Commands.UpdateBrand;
using Application.Features.Languages.Dtos;
using Application.Features.Languages.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Language, LanguageListDto>().ReverseMap();
            CreateMap<IPaginate<Language>, LanguageListViewModel>().ReverseMap();


            CreateMap<Language, LanguageGetByIdDto>().ReverseMap();

            CreateMap<Language, CreateLanguageCommand>().ReverseMap();
            CreateMap<Language,CreatedLanguageDto>().ReverseMap();

            CreateMap<Language, DeletedLanguageDto>().ReverseMap();

            CreateMap<Language, UpdatedLanguageDto>().ReverseMap();
            CreateMap<Language, UpdateLanguageCommand>().ReverseMap();

        }
    }
}
