using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.RegisterDeveloper
{
    public class RegsiterDeveloperCommandValidator : AbstractValidator<RegisterDeveloperCommand>
    {
        public RegsiterDeveloperCommandValidator()
        {
            RuleFor(x => x.RegisterDto.Email).NotEmpty();
            RuleFor(x => x.RegisterDto.FirstName).NotEmpty();
            RuleFor(x => x.RegisterDto.LastName).NotEmpty();
            RuleFor(x => x.RegisterDto.Password).NotEmpty();
            RuleFor(x => x.RegisterDto.Password).MinimumLength(6);
            RuleFor(x => x.RegisterDto.Password).MaximumLength(20);
        }
    }
}
