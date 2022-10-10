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
        private readonly IUserOperationClaimRepository userOperationClaimRepository;
        public UserOperationClaimBusinessRules(IDeveloperRepository developerRepository,IOperationClaimRepository operationClaimRepository, IUserOperationClaimRepository userOperationClaimRepository)
        {
            this.developerRepository = developerRepository;
            this.operationClaimRepository = operationClaimRepository;
            this.userOperationClaimRepository = userOperationClaimRepository;
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

        public async Task UserOperationClaimCannotBeDublicatedWhenInserted(int userId,int operationId)
        {
            UserOperationClaim result = await userOperationClaimRepository.GetAsync(x=>x.OperationClaimId==operationId && x.UserId==userId);
            if (result != null) throw new BusinessException("Values already in database.");
        }


    }
}
