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

namespace Application.Features.GithubAccounts.Commands.DeleteGithubAccount
{
    public class DeleteGithubAccountCommand : IRequest<DeletedGithubAccountDto>
    {
        public int Id { get; set; }
        public class DeleteGithubAccountCommandHamdler : IRequestHandler<DeleteGithubAccountCommand, DeletedGithubAccountDto>
        {
            private readonly IGithubAccountRepository githubAccountRepository;
            private readonly IMapper mapper;
            public DeleteGithubAccountCommandHamdler(IGithubAccountRepository githubAccountRepository, IMapper mapper)
            {
                this.mapper = mapper;
                this.githubAccountRepository = githubAccountRepository;
            }

            public async Task<DeletedGithubAccountDto> Handle(DeleteGithubAccountCommand request, CancellationToken cancellationToken)
            {

                GithubAccount account = await githubAccountRepository.GetAsync(x => x.Id == request.Id);


                GithubAccount deletedAccount = await githubAccountRepository.DeleteAsync(account);

                DeletedGithubAccountDto deletedGithubAccountDto = mapper.Map<DeletedGithubAccountDto>(deletedAccount);

                return deletedGithubAccountDto;
            }
        }

    }
}
