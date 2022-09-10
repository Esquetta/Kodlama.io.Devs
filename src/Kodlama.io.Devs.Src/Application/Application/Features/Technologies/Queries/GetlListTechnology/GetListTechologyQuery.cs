using Application.Features.Technologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Queries.GetlListTechonlogy
{
    public class GetListTechologyQuery : IRequest<TechonlogyListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListTechologyQueryHandler : IRequestHandler<GetListTechologyQuery, TechonlogyListModel>
        {
            private readonly IMapper mapper;
            private readonly ITechnologyRepository technologyRepository;
            public GetListTechologyQueryHandler(IMapper mapper, ITechnologyRepository technologyRepository)
            {
                this.mapper = mapper;
                this.technologyRepository = technologyRepository;
            }

            public async Task<TechonlogyListModel> Handle(GetListTechologyQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Technology> techology = await technologyRepository.
                      GetListAsync(include: x => x.Include(x => x.Language),
                      index: request.PageRequest.Page,
                      size: request.PageRequest.PageSize);

                TechonlogyListModel techonlogyListModel = mapper.Map<TechonlogyListModel>(techology);

                return techonlogyListModel;

            }
        }
    }
}
