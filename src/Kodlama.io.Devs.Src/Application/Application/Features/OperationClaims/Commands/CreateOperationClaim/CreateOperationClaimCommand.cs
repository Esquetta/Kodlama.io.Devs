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

namespace Application.Features.OperationClaims.Commands.CreateOperationClaim
{
    public class CreateOperationClaimCommand:IRequest<CreatedOperationClaimDto>
    {
        public string Name { get; set; }

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimDto>
        {
            private readonly IOperationClaimRepository operationClaimRepository;
            private readonly IMapper mapper;
            private readonly OperationClaimBusinesRules operationClaimBusinesRules;

            public async Task<CreatedOperationClaimDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await operationClaimBusinesRules.OperationCannotBeDuplicatedWhenInserted(request.Name);

                OperationClaim operationClaim = mapper.Map<OperationClaim>(request);

                OperationClaim addedOperationClaim = await operationClaimRepository.AddAsync(operationClaim);

                CreatedOperationClaimDto createdOperationClaimDto = mapper.Map<CreatedOperationClaimDto>(addedOperationClaim);

                return createdOperationClaimDto;
            }
        }
    }
}
