using Application.Features.OperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Queries.GetListOperationClaim
{
    public class GetListOperationClaimQuery : IRequest<OperationClaimListViewModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListOperationClaimQueryHandler : IRequestHandler<GetListOperationClaimQuery, OperationClaimListViewModel>
        {
            private readonly IOperationClaimRepository  operationClaimRepository;
            private readonly IMapper mapper;
            public GetListOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
            {
                this.operationClaimRepository = operationClaimRepository;
                this.mapper = mapper;
            }

            public async Task<OperationClaimListViewModel> Handle(GetListOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> OperationClaim = await operationClaimRepository.GetListAsync(size:request.PageRequest.PageSize,index:request.PageRequest.Page);

                OperationClaimListViewModel operationClaimListViewModel = mapper.Map<OperationClaimListViewModel>(OperationClaim);

                return operationClaimListViewModel;
                
            }
        }
    }
}
