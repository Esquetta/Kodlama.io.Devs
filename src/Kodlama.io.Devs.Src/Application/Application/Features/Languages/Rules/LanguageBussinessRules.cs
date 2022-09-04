using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Rules
{
    public class LanguageBussinessRules
    {
        private readonly ILanguageRepository languageRepository;
        public LanguageBussinessRules(ILanguageRepository languageRepository)
        {
            this.languageRepository = languageRepository;
        }
        public void LanguageShouldExistWhenRequested(Language language)
        {

            if (language == null) throw new BusinessException("Requested language does not exists.");
        }
        public async Task LanguageNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Language> result = await languageRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Language name exists.");
        }
        public async Task<Language> IsLanguageNameExist(string name)
        {
            Language result = await languageRepository.GetAsync(b => b.Name == name);
            if (result == null) throw new BusinessException("Language not found.");
            return result;
        }
        public async Task LanguageNameCanNotBeDuplicatedWhenUpdated(string name)
        {
            IPaginate<Language> result = await languageRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Language name exists.");
        }
    }
}
