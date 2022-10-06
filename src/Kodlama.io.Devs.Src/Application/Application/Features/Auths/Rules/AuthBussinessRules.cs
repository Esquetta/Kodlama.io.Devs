using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Rules
{
    public class AuthBussinessRules
    {
        private readonly IDeveloperRepository developerRepository;
        public AuthBussinessRules(IDeveloperRepository developerRepository)
        {
            this.developerRepository = developerRepository;
        }
        public async Task IsDeveloperExist(string email)
        {
            Developer user = await developerRepository.GetAsync(x=>x.Email==email);
            if (user!=null)  throw new BusinessException("Email taken  try new one.");
        }
        public async Task UserCheckForLogin(string email)
        {
            Developer user = await developerRepository.GetAsync(x => x.Email == email);
            if (user == null) throw new BusinessException("Invalid username or password.");
        }
    }
}
