using Application.Features.Languages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Queries.GetListLanguageQuery
{
    public class GetListLanguageQuery : IRequest<LanguageListViewModel>
    {
        public PageRequest pageRequest { get; set; }

        public class GetListLanguageQueryHandler : IRequestHandler<GetListLanguageQuery, LanguageListViewModel>
        {
            private readonly ILanguageRepository languageRepository;
            private readonly IMapper mapper;

            public GetListLanguageQueryHandler(ILanguageRepository languageRepository, IMapper mapper)
            {
                this.languageRepository = languageRepository;
                this.mapper = mapper;
            }

            public async Task<LanguageListViewModel> Handle(GetListLanguageQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Language> paginate = await languageRepository.GetListAsync(index:request.pageRequest.Page,size:request.pageRequest.PageSize);

                LanguageListViewModel mappedModel = mapper.Map<LanguageListViewModel>(paginate);


                return mappedModel;


            }
        }
    }
}
