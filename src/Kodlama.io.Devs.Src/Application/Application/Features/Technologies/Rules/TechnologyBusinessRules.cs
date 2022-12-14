using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository technologyRepository;
        public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
        {
            this.technologyRepository = technologyRepository;
        }
        public async Task TechnologyNameCanNotBeDuplicatedWhenUpdated(string name)
        {
            IPaginate<Technology> result = await technologyRepository.GetListAsync(b =>b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Technology name exists.");
        }
        public async Task TechnologyNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Technology> result = await technologyRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Technology name exists.");
        }
        
        public async Task<Technology> IsTechnologyNameExist(string name)
        {
            Technology result = await technologyRepository.GetAsync(b => b.Name == name);
            if (result == null) throw new BusinessException("Technology not found.");
            return result;
        }

        public async Task<Technology> IsTechnologyExist(int Id)
        {
            Technology result = await technologyRepository.GetAsync(b => b.Id == Id );
            if (result == null) throw new BusinessException("Technology not found.");
            return result;
        }
    }
}
