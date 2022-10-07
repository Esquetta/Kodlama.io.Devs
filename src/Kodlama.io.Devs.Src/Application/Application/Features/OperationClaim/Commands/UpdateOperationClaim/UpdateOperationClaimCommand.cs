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

namespace Application.Features.OperationClaims.Commands.UpdateOperationClaim
{
    public class UpdateOperationClaimCommand:IRequest<UpdatedOperationClaimDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimDto>
        {

            private readonly IOperationClaimRepository operationClaimRepository;
            private readonly IMapper mapper;
            private readonly OperationClaimBusinesRules operationClaimBusinesRules;
            public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinesRules operationClaimBusinesRules)
            {
                this.operationClaimRepository = operationClaimRepository;
                this.mapper = mapper;
                this.operationClaimBusinesRules = operationClaimBusinesRules;
            }

            public async Task<UpdatedOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await operationClaimBusinesRules.OperationCannotBeDuplicatedWhenUpdated(request.Name);

                OperationClaim operationClaim = mapper.Map<OperationClaim>(request);

                OperationClaim updatedOperationClaim = await operationClaimRepository.UpdateAsync(operationClaim);

                UpdatedOperationClaimDto updatedOperationClaimDto = mapper.Map<UpdatedOperationClaimDto>(updatedOperationClaim);

                return updatedOperationClaimDto;
            }
        }
    }
}
