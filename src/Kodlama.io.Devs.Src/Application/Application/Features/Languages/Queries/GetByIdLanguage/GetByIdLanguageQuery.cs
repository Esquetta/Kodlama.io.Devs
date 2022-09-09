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

namespace Application.Features.Languages.Queries.GetByIdLanguageQuery
{
    public class GetByIdLanguageQuery : IRequest<LanguageGetByIdDto>
    {
        public int Id { get; set; }


        public class GetByIdLanguageQueryHandler : IRequestHandler<GetByIdLanguageQuery, LanguageGetByIdDto>
        {
            private readonly ILanguageRepository languageRepository;
            private readonly IMapper mapper;
            private readonly LanguageBussinessRules languageBussinessRules;

            public GetByIdLanguageQueryHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBussinessRules languageBussinessRules)
            {
                this.languageRepository = languageRepository;
                this.mapper = mapper;
                this.languageBussinessRules = languageBussinessRules;
            }
            public async Task<LanguageGetByIdDto> Handle(GetByIdLanguageQuery request, CancellationToken cancellationToken)
            {
                Language language = await languageRepository.GetAsync(x => x.Id == request.Id);

                languageBussinessRules.LanguageShouldExistWhenRequested(language);

                LanguageGetByIdDto languageGetByIdDto = mapper.Map<LanguageGetByIdDto>(language);

                return languageGetByIdDto;


            }
        }
    }
}
