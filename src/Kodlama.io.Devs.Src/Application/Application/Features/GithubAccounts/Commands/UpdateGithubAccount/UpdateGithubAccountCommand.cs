using Application.Features.GithubAccounts.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubAccounts.Commands.UpdateGithubAccount
{
    public class UpdateGithubAccountCommand : IRequest<UpdatedGithubAccountDto>
    {
        public int Id { get; set; }
        public class UpdateGithubAccountCommandHamdler : IRequestHandler<UpdateGithubAccountCommand, UpdatedGithubAccountDto>
        {
            private readonly IGithubAccountRepository githubAccountRepository;
            private readonly IMapper mapper;
            public UpdateGithubAccountCommandHamdler(IGithubAccountRepository githubAccountRepository, IMapper mapper)
            {
                this.mapper = mapper;
                this.githubAccountRepository = githubAccountRepository;
            }

            public async Task<UpdatedGithubAccountDto> Handle(UpdateGithubAccountCommand request, CancellationToken cancellationToken)
            {

                GithubAccount account = await githubAccountRepository.GetAsync(x => x.Id == request.Id);

                GithubAccount updatedAccount = await githubAccountRepository.DeleteAsync(account);

                UpdatedGithubAccountDto updatedGithubAccountDto = mapper.Map<UpdatedGithubAccountDto>(updatedAccount);

                return updatedGithubAccountDto;
            }
        }

    }
}
