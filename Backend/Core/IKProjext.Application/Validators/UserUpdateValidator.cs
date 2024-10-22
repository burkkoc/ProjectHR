using FluentValidation;
using IKProject.Application.Features.Commands.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Validators
{
    public class UserUpdateValidator : AbstractValidator<UpdateCommand>
    {
        public UserUpdateValidator()
        {

            RuleFor(c => c.Address)
               .NotEmpty()
                   .WithMessage("Lütfen adresinizi girin.")
               .NotNull()
                   .WithMessage("Lütfen adresinizi girin.")
               .MinimumLength(10)
                   .WithMessage("Adres 10 ile 250 karakter arasında olmalıdır.")
               .MaximumLength(250)
                   .WithMessage("Adres 10 ile 250 karakter arasında olmalıdır.");

            RuleFor(c => c.PhoneNumber)
               .NotEmpty()
                   .WithMessage("Lütfen telefon numaranızı girin.")
               .NotNull()
                   .WithMessage("Lütfen telefon numaranızı girin.")
               .Length(11)
                   .WithMessage("Telefon numarası 11 haneli olmalıdır. Örnek: 05051234567")
               .Matches("^[0-9]+$")
                   .WithMessage("Lütfen telefon numaranızı yalnızca sayılardan oluşacak şekilde girin.");
        }
    }
}
