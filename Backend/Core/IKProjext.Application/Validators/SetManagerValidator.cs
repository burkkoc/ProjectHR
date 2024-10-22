using FluentValidation;
using IKProject.Application.Features.Commands.SetManagerToCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Validators
{
    public class SetManagerValidator : AbstractValidator<SetManagerCommand>
    {
        public SetManagerValidator()
        {
            RuleFor(sm => sm.UserEmail)
                .NotEmpty()
                .WithMessage("Lütfen şirket emaili giriniz.")
                .NotNull()
                    .WithMessage("Lütfen şirket emaili giriniz.")
                .Must(ExternalMethodsForValidators.IsMailValid)
                    .WithMessage("Lütfen geçerli bir email adresi giriniz.");

            RuleFor(sm => sm.CompanyEmail)
                .NotEmpty()
                    .WithMessage("Lütfen şirket emaili giriniz.")
                .NotNull()
                    .WithMessage("Lütfen şirket emaili giriniz.")
                .Must(ExternalMethodsForValidators.IsMailValid)
                    .WithMessage("Lütfen geçerli bir email adresi giriniz.");
        }
    }
}
