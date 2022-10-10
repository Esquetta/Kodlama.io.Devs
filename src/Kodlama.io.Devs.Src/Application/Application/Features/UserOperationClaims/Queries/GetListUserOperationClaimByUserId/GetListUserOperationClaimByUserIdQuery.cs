using Application.Features.UserOperationClaims.Models;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Queries.GetListUserOperationClaimByUserId
{
    public class GetListUserOperationClaimByUserIdQuery : IRequest<UserOperationClaimListViewModel>
    {
        public int UserId { get; set; }

        public class GetListUserOperationClaimByUserIdQueryHandler : IRequestHandler<GetListUserOperationClaimByUserIdQuery, UserOperationClaimListViewModel>
        {
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IMapper mapper;
            private readonly UserOperationClaimBusinessRules userOperationClaimBusinessRules;
            public GetListUserOperationClaimByUserIdQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                this.mapper = mapper;
                this.userOperationClaimBusinessRules = userOperationClaimBusinessRules;
                this.userOperationClaimRepository = userOperationClaimRepository;
            }

            public async Task<UserOperationClaimListViewModel> Handle(GetListUserOperationClaimByUserIdQuery request, CancellationToken cancellationToken)
            {
                await userOperationClaimBusinessRules.IsUserExist(request.UserId);

                IPaginate<UserOperationClaim> paginate = await userOperationClaimRepository.GetListAsync(include:x=>x.Include(x=>x.User).Include(x=>x.OperationClaim),predicate:x=>x.UserId==request.UserId);

                UserOperationClaimListViewModel userOperationClaimListViewModel = mapper.Map<UserOperationClaimListViewModel>(paginate);

                return userOperationClaimListViewModel;
            }
        }
    }
}
