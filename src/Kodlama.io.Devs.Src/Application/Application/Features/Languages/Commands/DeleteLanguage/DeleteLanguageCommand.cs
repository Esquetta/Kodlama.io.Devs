using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommand : IRequest<DeletedLanguageDto>
    {
        public string Name { get; set; }
        public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, DeletedLanguageDto>
        {
            private readonly ILanguageRepository languageRepository;
            private readonly IMapper mapper;
            private readonly LanguageBusinessRules languageBussinessRules;
            public DeleteLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules languageBussinessRules)
            {
                this.languageRepository = languageRepository;
                this.mapper = mapper;
                this.languageBussinessRules = languageBussinessRules;
            }

            public async Task<DeletedLanguageDto> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
            {
                Language checkedLanguage = await languageBussinessRules.IsLanguageNameExist(request.Name);

                Language deletedLanguage = await languageRepository.DeleteAsync(checkedLanguage);

                DeletedLanguageDto deletedLanguageDto = mapper.Map<DeletedLanguageDto>(deletedLanguage);

                return deletedLanguageDto;
                
            }
        }
    }
}
