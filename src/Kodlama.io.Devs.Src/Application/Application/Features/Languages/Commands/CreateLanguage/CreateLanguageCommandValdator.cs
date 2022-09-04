using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommandValdator:AbstractValidator<CreateLanguageCommand>
    {
        public CreateLanguageCommandValdator()
        {
            RuleFor(x=>x.Name).MinimumLength(2);
            RuleFor(x=>x.Name).NotEmpty();
        }
    }
}
