using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Rules
{
    public class DeveloperBussinessRules
    {
        private readonly IDeveloperRepository developerRepository;
        public DeveloperBussinessRules(IDeveloperRepository developerRepository)
        {
            this.developerRepository = developerRepository;
        }
        public async Task IsDeveloperExist(string email)
        {
            Developer dev = await developerRepository.GetAsync(x=>x.Email==email);
            if (dev!=null)  throw new BusinessException("Email taken  try new one.");
        }
    }
}
