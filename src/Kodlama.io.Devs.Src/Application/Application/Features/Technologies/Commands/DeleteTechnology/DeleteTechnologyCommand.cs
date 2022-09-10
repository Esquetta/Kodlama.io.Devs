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

namespace Application.Features.Technologies.Commands.DeleteTechnology
{
    public class DeleteTechnologyCommand : IRequest<DeletedTechnologyDto>
    {
        public string Name { get; set; }

        public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeletedTechnologyDto>
        {
            private readonly ITechnologyRepository technologyRepository;
            private readonly IMapper mapper;
            private readonly TechnologyBussinessRules technologyBussinessRules;
            public DeleteTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBussinessRules technologyBussinessRules)
            {
                this.technologyRepository = technologyRepository;
                this.mapper = mapper;
                this.technologyBussinessRules = technologyBussinessRules;
            }

            public async Task<DeletedTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
            {
                Technology technology = await technologyBussinessRules.IsTechnologyNameExist(request.Name);


                Technology deletedTechnology = await technologyRepository.DeleteAsync(technology);

                DeletedTechnologyDto deletedTechnologyDto = mapper.Map<DeletedTechnologyDto>(deletedTechnology);

                return deletedTechnologyDto;
            }
        }
    }
}
