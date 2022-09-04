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

namespace Application.Features.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommand : IRequest<CreatedLanguageDto>
    {
        public string Name { get; set; }

        public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, CreatedLanguageDto>
        {
            private readonly ILanguageRepository languageRepository;
            private readonly IMapper mapper;
            private readonly LanguageBussinessRules languageBussinessRules;
            public CreateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBussinessRules languageBussinessRules)
            {
                this.languageRepository = languageRepository;
                this.mapper = mapper;
                this.languageBussinessRules = languageBussinessRules;
            }

            public async Task<CreatedLanguageDto> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
            {
                await languageBussinessRules.LanguageNameCanNotBeDuplicatedWhenInserted(request.Name);

                Language mappedLanguage = mapper.Map<Language>(request);
                Language createdLanguage = await languageRepository.AddAsync(mappedLanguage);

                CreatedLanguageDto createdLanguageDto = mapper.Map<CreatedLanguageDto>(createdLanguage);


                return createdLanguageDto;

            }
        }
    }
}
