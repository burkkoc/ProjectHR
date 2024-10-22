using FluentValidation;
using IKProject.Application.Features.Commands.Update;
using IKProject.Application.Features.PasswordReset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Validators
{
    public class PasswordResetValidator : AbstractValidator<PasswordResetCommandRequest>
    {
        public PasswordResetValidator()
        {
            RuleFor(c => c.NewPassword)
                    .NotEmpty()
                        .WithMessage("Lütfen parolanızı girin.")
                    .NotNull()
                        .WithMessage("Lütfen parolanızı girin.")
                    .Must(ExternalMethodsForValidators.IsPasswordValid)
                        .WithMessage("Parolanızda en az iki büyük, iki küçük, iki sayı ve iki özel karakter bulunmalıdır.")
                    .MinimumLength(8)
                        .WithMessage("Parolanız 8 ile 25 karakter arasında olmalıdır.")
                    .MaximumLength(25)
                        .WithMessage("Parolanız 8 ile 25 karakter arasında olmalıdır.");

            RuleFor(c => c.Email)
                .NotNull()
                    .WithMessage("Lütfen mail adresinizi girin.")
                .NotEmpty()
                    .WithMessage("Lütfen mail adresinizi girin.")
                .Must(ExternalMethodsForValidators.IsMailValid)
                    .WithMessage("Mail adresiniz ornek@ornek.com/net/org/com.tr/edu.tr formatında olmalıdır.")
                .MinimumLength(5)
                    .WithMessage("Mail adresiniz 5 ile 25 karakter arasında olmalıdır.")
                .MaximumLength(25)
                    .WithMessage("Mail adresiniz 5 ile 25 karakter arasında olmalıdır.");

            //RuleFor(c => c.VerificationCode)
            //    .NotNull()
            //        .WithMessage("Lütfen doğrulama kodunuzu girin.")
            //    .NotEmpty()
            //        .WithMessage("Lütfen doğrulama kodunuzu girin.")
            //    .Length(6)
            //        .WithMessage("Doğrulama kodunuz 6 haneli olmalıdır.")
            //    .Matches("^[0-9]+$")
            //        .WithMessage("Doğrulama kodu yalnızca sayılardan oluşmalıdır.");
        }
    }
}
