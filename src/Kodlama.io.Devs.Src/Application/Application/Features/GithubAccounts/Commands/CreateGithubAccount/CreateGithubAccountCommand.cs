using Application.Features.GithubAccounts.Dtos;
using Application.Features.GithubAccounts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubAccounts.Commands.CreateGithubAccount
{
    public class CreateGithubAccountCommand : IRequest<CreatedGithibAccountDto>
    {
        public string AccountLink { get; set; }

        public class CreateGithubAccountCommandHandler : IRequestHandler<CreateGithubAccountCommand, CreatedGithibAccountDto>
        {
            private readonly IGithubAccountRepository githubAccountRepository;
            private readonly IMapper mapper;
            private readonly GithubAccountBussinessRules githubAccountBussinessRules;
            private readonly IHttpContextAccessor httpContextAccessor;
            private readonly IDeveloperRepository developerRepository;
            public CreateGithubAccountCommandHandler(IGithubAccountRepository githubAccountRepository, IMapper mapper, GithubAccountBussinessRules githubAccountBussinessRules, IHttpContextAccessor httpContextAccessor, IDeveloperRepository developerRepository)
            {
                this.githubAccountRepository = githubAccountRepository;
                this.mapper = mapper;
                this.githubAccountBussinessRules = githubAccountBussinessRules;
                this.httpContextAccessor = httpContextAccessor;
                this.developerRepository = developerRepository;
            }

            public async Task<CreatedGithibAccountDto> Handle(CreateGithubAccountCommand request, CancellationToken cancellationToken)
            {
                await githubAccountBussinessRules.ConnotBeDuplicatedWhenInserted(request.AccountLink);

                GithubAccount mappedAccount = mapper.Map<GithubAccount>(request);

                var name = httpContextAccessor.HttpContext.User.Identity.Name;

                Developer user = await developerRepository.GetAsync(x => x.FirstName == name);
                if (user == null)
                    return null;

                GithubAccount creadtedAccount = await githubAccountRepository.AddAsync(mappedAccount);

                user.AccountId = creadtedAccount.Id;
                await developerRepository.UpdateAsync(user);

                CreatedGithibAccountDto createdGithibAccountDto = mapper.Map<CreatedGithibAccountDto>(creadtedAccount);

                return createdGithibAccountDto;

            }
        }
    }
}
