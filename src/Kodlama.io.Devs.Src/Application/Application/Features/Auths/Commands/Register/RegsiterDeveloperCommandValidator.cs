using Application.Features.Auths.Commands.Register;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.RegisterDeveloper
{
    public class RegsiterDeveloperCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegsiterDeveloperCommandValidator()
        {
            RuleFor(x => x.UserForRegisterDto.Email).NotEmpty();
            RuleFor(x => x.UserForRegisterDto.FirstName).NotEmpty();
            RuleFor(x => x.UserForRegisterDto.LastName).NotEmpty();
            RuleFor(x => x.UserForRegisterDto.Password).NotEmpty();
            RuleFor(x => x.UserForRegisterDto.Password).MinimumLength(6);
            RuleFor(x => x.UserForRegisterDto.Password).MaximumLength(20);
        }
    }
}
