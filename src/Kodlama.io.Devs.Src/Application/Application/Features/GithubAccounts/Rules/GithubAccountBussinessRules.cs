using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubAccounts.Rules
{
    public class GithubAccountBussinessRules
    {
        private readonly IGithubAccountRepository githubAccountRepository;
        public GithubAccountBussinessRules(IGithubAccountRepository githubAccountRepository)
        {
            this.githubAccountRepository = githubAccountRepository;
        }
        public async Task ConnotBeDuplicatedWhenInserted(string accountLink)
        {
            GithubAccount githubAccount = await githubAccountRepository.GetAsync(x => x.AccountLink == accountLink);
            if (githubAccount != null) throw new BusinessException("GithubAccount Exist");
        }

    }
}
