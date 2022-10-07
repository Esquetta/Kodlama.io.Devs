using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinesRules
    {
        private readonly IOperationClaimRepository operationClaimRepository;
        public OperationClaimBusinesRules(IOperationClaimRepository operationClaimRepository)
        {
            this.operationClaimRepository = operationClaimRepository;
        }

        public async Task OperationCannotBeDuplicatedWhenInserted(string name)
        {
            OperationClaim operationClaim = await operationClaimRepository.GetAsync(x => x.Name == name);
            if (operationClaim != null) throw new BusinessException("Operation name exist.");

        }
        public async Task OperationCannotBeDuplicatedWhenUpdated(string name)
        {
            OperationClaim operationClaim = await operationClaimRepository.GetAsync(x => x.Name == name);
            if (operationClaim != null) throw new BusinessException("Operation name exist.");

        }
    }
}
