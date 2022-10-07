using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateOperationClaimCommand, OperationClaim>().ReverseMap();
            CreateMap<CreatedOperationClaimDto, OperationClaim>().ReverseMap();
            CreateMap<DeletedOperationClaimDto,OperationClaim>().ReverseMap();
            CreateMap<UpdateOperationClaimCommand, OperationClaim>().ReverseMap();
            CreateMap<UpdatedOperationClaimDto, OperationClaim>().ReverseMap();
            CreateMap<OperationClaimListDto, OperationClaim>().ReverseMap();

            CreateMap<IPaginate<OperationClaim>, OperationClaimListViewModel>().ReverseMap();
        }
    }
}
