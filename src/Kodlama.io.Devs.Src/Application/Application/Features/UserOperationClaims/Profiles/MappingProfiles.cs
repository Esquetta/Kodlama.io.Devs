using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateUserOperationClaimCommand, UserOperationClaim>().ReverseMap();
            CreateMap<CreatedUserOperationClaimDto, UserOperationClaim>().ReverseMap();

            CreateMap<UserOperationClaim, UserOperationsClaimsListDto>().ForMember(x => x.OperationName, opt => opt.MapFrom(src => src.OperationClaim.Name));

            CreateMap<IPaginate<UserOperationClaim>, UserOperationClaimListViewModel>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.Items[0].User.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.Items[0].User.LastName))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Items[0].User.Email))
                .ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Items))
                .ReverseMap();
        }
    }
}
