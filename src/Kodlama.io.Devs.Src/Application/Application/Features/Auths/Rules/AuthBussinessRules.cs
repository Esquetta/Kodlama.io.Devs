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
        private readonly IUserRepository userRepository;
        public AuthBussinessRules(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task IsDeveloperExist(string email)
        {
            User user = await userRepository.GetAsync(x=>x.Email==email);
            if (user!=null)  throw new BusinessException("Email taken  try new one.");
        }
    }
}
