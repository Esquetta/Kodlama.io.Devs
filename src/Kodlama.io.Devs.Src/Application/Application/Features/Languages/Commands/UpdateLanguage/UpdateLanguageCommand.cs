using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.UpdateBrand
{
    public class UpdateLanguageCommand : IRequest<UpdatedLanguageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, UpdatedLanguageDto>
        {
            private readonly ILanguageRepository languageRepository;
            private readonly IMapper mapper;
            private readonly LanguageBussinessRules languageBussinessRules;
            public UpdateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBussinessRules languageBussinessRules)
            {
                this.languageRepository = languageRepository;
                this.mapper = mapper;
                this.languageBussinessRules = languageBussinessRules;
            }

            public async Task<UpdatedLanguageDto> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
            {
                await languageBussinessRules.LanguageNameCanNotBeDuplicatedWhenUpdated(request.Name);
                Language mappedLanguage = mapper.Map<Language>(request);

                Language updatedLanguage = await languageRepository.UpdateAsync(mappedLanguage);

                UpdatedLanguageDto updatedLanguageDto = mapper.Map<UpdatedLanguageDto>(updatedLanguage);

                return updatedLanguageDto;


            }
        }
    }
}
