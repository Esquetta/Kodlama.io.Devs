using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyCommand : IRequest<CreatedTechnologyDto>
    {
        public string Name { get; set; }
        public int LanguageId { get; set; }

        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
        {
            private readonly ITechnologyRepository technologyRepository;
            private readonly IMapper mapper;
            private readonly TechnologyBusinessRules technologyBussinessRules;
            public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBussinessRules)
            {
                this.technologyRepository = technologyRepository;
                this.mapper = mapper;
                this.technologyBussinessRules = technologyBussinessRules;
            }

            public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await technologyBussinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);
                Technology mappedTechnology = mapper.Map<Technology>(request);

                Technology addedTechnology = await technologyRepository.AddAsync(mappedTechnology);

                CreatedTechnologyDto createdBrandDto = mapper.Map<CreatedTechnologyDto>(addedTechnology);

                return createdBrandDto;


            }
        }
    }
}
