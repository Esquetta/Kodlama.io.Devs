using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.DeleteOperationClaim
{
    public class DeleteOperationClaimCommand:IRequest<DeletedOperationClaimDto>
    {
        public int Id { get; set; }


        public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>
        {
            private readonly IOperationClaimRepository operationClaimRepository;
            private readonly IMapper mapper;
           
            public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
            {
                this.operationClaimRepository = operationClaimRepository;
                this.mapper = mapper;
                
            }

            public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim operationClaim = await operationClaimRepository.GetAsync(x => x.Id == request.Id);


                OperationClaim DeletedOperationClaim = await operationClaimRepository.DeleteAsync(operationClaim);

                DeletedOperationClaimDto deletedOperationClaimDto =  mapper.Map<DeletedOperationClaimDto>(DeletedOperationClaim);

                return deletedOperationClaimDto;
            }
        }

    }
}
