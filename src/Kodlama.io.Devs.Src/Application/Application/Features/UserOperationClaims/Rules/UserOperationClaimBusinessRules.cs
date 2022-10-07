using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
        
        private readonly IOperationClaimRepository operationClaimRepository;
        private readonly IDeveloperRepository developerRepository;
        public UserOperationClaimBusinessRules(IDeveloperRepository developerRepository,IOperationClaimRepository operationClaimRepository)
        {
            this.developerRepository = developerRepository;
            this.operationClaimRepository = operationClaimRepository;
            
        }

        public async Task IsOperationExist(int operationId)
        {
            OperationClaim result = await operationClaimRepository.GetAsync(x => x.Id == operationId);
            if(result==null)  throw new BusinessException("Operation is invlaid");
        }
        public async Task IsUserExist(int userId)
        {
            Developer result = await developerRepository.GetAsync(x => x.Id == userId);
            if (result == null) throw new BusinessException("User not found");
        }

       
    }
}
