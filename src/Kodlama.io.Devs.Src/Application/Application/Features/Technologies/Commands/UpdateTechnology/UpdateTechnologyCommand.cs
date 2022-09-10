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

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand:IRequest<UpdatedTechnologyDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }

        public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
        {
            private readonly ITechnologyRepository technologyRepository;
            private readonly IMapper mapper;
            private readonly TechnologyBussinessRules technologyBussinessRules;
            public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBussinessRules technologyBussinessRules)
            {
                this.technologyRepository = technologyRepository;
                this.mapper = mapper;
                this.technologyBussinessRules = technologyBussinessRules;
            }

            public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await technologyBussinessRules.TechnologyNameCanNotBeDuplicatedWhenUpdated(request.Name);

                Technology mappedTechnology = mapper.Map<Technology>(request);

                Technology updatedTechnology = await technologyRepository.UpdateAsync(mappedTechnology);

                UpdatedTechnologyDto updatedTechnologyDto = mapper.Map<UpdatedTechnologyDto>(updatedTechnology);

                return updatedTechnologyDto;

            }
        }
    }
}
