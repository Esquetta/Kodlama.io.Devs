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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubAccounts.Commands.CreateGithubAccount
{
    public class CreateGithubAccountCommand : IRequest<CreatedGithibAccountDto>
    {
        public string AccountLink { get; set; }
        public string Email { get; set; }

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

                var email = request.Email;
                Developer user = await developerRepository.GetAsync(x => x.Email == email);
                if (user == null)
                    return null;

                GithubAccount creadtedAccount = await githubAccountRepository.AddAsync(mappedAccount);

                user.GithubAccountId = creadtedAccount.Id;
                await developerRepository.UpdateAsync(user);

                CreatedGithibAccountDto createdGithibAccountDto = mapper.Map<CreatedGithibAccountDto>(creadtedAccount);

                return createdGithibAccountDto;

            }
            private string GetCurrentUser()
            {
                var identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

                if (identity != null)
                {
                    var userClaims = identity.Claims;

                    return userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;


                }
                return null;
            }
        }
    }
}
