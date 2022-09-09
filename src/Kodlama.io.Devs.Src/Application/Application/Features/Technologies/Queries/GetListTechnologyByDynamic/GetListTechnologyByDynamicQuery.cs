using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetlListTechonlogy;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Queries.GetListTechnologyByDynamic
{
    public class GetListTechnologyByDynamicQuery: IRequest<TechonlogyListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListTechnologyByDynamicQueryHandler : IRequestHandler<GetListTechnologyByDynamicQuery, TechonlogyListModel>
        {
            private readonly IMapper mapper;
            private readonly ITechnologyRepository technologyRepository;
            public GetListTechnologyByDynamicQueryHandler(IMapper mapper, ITechnologyRepository technologyRepository)
            {
                this.mapper = mapper;
                this.technologyRepository = technologyRepository;
            }

            public async Task<TechonlogyListModel> Handle(GetListTechnologyByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Technology> techology = await technologyRepository.
                      GetListByDynamicAsync(request.Dynamic,
                      include: x => x.Include(x => x.Language),
                      index: request.PageRequest.Page,
                      size: request.PageRequest.PageSize);

                TechonlogyListModel techonlogyListModel = mapper.Map<TechonlogyListModel>(techology);

                return techonlogyListModel;

            }
        }
    }
}
