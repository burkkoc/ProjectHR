using FluentValidation;
using FluentValidation.Validators;
using IKProject.Application.Features.Commands.RegisterCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Validators
{
    public class CompanyRegisterValidator : AbstractValidator<RegisterCompanyCommand>
    {
        public CompanyRegisterValidator()
        {
            //RuleFor(c => c.Title)
            //    .NotEmpty()
            //        .WithMessage("Lütfen şirket ünvanını girin.")
            //    .NotNull()
            //        .WithMessage("Lütfen şirket ünvanını girin.")
            //    .MinimumLength(2)
            //        .WithMessage("Ünvan 2 ile 50 karakter arasında olmalıdır")
            //    .MaximumLength(50)
            //        .WithMessage("Ünvan 2 ile 50 karakter arasında olmalıdır")
            //    .Matches("^[a-zA-ZçğıöşüÇĞİÖŞÜ0-9&+\\-.\\s]+$")
            //        .WithMessage("Ünvanda yalnızca sayılar, harfler ve (&, +, -, .) sembolleri bulunabilir");

            RuleFor(c => c.MersisNo)
                .NotEmpty()
                    .WithMessage("Mersis numarası boş bırakılamaz.")
                .NotNull()
                    .WithMessage("Mersis numarası boş bırakılamaz.")
                .Length(16)
                    .WithMessage("Mersis numarası 16 hane uzunluğunda olmalıdır.")
                .Matches("^[0-9]+$")
                    .WithMessage("Mersis numarası yalnızca sayıları barındırabilir.");

            RuleFor(c => c.Email)
                .NotNull()
                    .WithMessage("Lütfen şirket email adresini girin.")
                .NotEmpty()
                    .WithMessage("Lütfen şirket email adresini girin.")
                .Must(ExternalMethodsForValidators.IsMailValid)
                    .WithMessage("Şirket email adresiniz ornek@ornek.com/net/org/com.tr/edu.tr formatında olmalıdır.")
                .MinimumLength(5)
                    .WithMessage("Şirket email adresiniz 5 ile 50 karakter arasında olmalıdır.")
                .MaximumLength(50)
                    .WithMessage("Şirket email adresiniz 5 ile 50 karakter arasında olmalıdır.");
                    

            RuleFor(c=>c.Address)
                .NotEmpty()
                    .WithMessage("Lütfen şirket adresini girin.")
                .NotNull()
                    .WithMessage("Lütfen şirket adresini girin.")
                .MinimumLength(10)
                    .WithMessage("Şirket adresi 10 ile 250 karakter arasında olmalıdır.")
                .MaximumLength(250)
                    .WithMessage("Şirket adresi 10 ile 250 karakter arasında olmalıdır.");

            RuleFor(c => c.TaxNo)
                .NotEmpty()
                    .WithMessage("Lütfen vergi numarasını girin.")
                .NotNull()
                    .WithMessage("Lütfen vergi numarasını girin.")
                .Length(10)
                    .WithMessage("Vergi numarası 10 haneli olmalıdır.")
                .Matches("^[0-9]+$")
                    .WithMessage("Vergi numarası sayılardan oluşmalıdır.")
                .Must(ExternalMethodsForValidators.IsTaxNoValid)
                    .WithMessage("Vergi numarası geçersiz. Lütfen geçerli bir vergi numarası girin.");

            RuleFor(c=>c.ContractEndDate)
                .NotEmpty()
                    .WithMessage("Lütfen kontrat bitiş tarihini girin")
                .NotNull()
                    .WithMessage("Lütfen kontrat bitiş tarihini girin")
                .GreaterThanOrEqualTo(DateTime.UtcNow.AddYears(-100))
                    .WithMessage("Lütfen geçerli bir kontrat bitiş tarihi girin.")
                .GreaterThan(c => c.ContractStartDate)
                    .WithMessage("Kontrat bitiş tarihi, başlama tarihinden önce olamaz.");

            RuleFor(c => c.ContractStartDate)
                .NotEmpty()
                    .WithMessage("Lütfen kontrat başlama tarihini girin")
                .NotNull()
                    .WithMessage("Lütfen kontrat başlama tarihini girin")
                .GreaterThanOrEqualTo(DateTime.UtcNow.AddYears(-100))
                    .WithMessage("Lütfen geçerli bir kontrat başlama tarihi girin.")
                .LessThan(c => c.ContractEndDate)
                    .WithMessage("Kontrat başlama tarihi, bitiş tarihinden sonra olamaz.");

            RuleFor(c => c.FoundationYear)
                .NotEmpty()
                    .WithMessage("Lütfen şirket kuruluş tarihini girin")
                .NotNull()
                    .WithMessage("Lütfen şirket kuruluş tarihini girin")
                .GreaterThanOrEqualTo(DateTime.Now.AddYears(-100).Year)
                    .WithMessage("Lütfen geçerli bir tarih girin.")
                .LessThanOrEqualTo(DateTime.Now.Year)
                    .WithMessage("Lütfen geçerli bir tarih girin.")
                .LessThanOrEqualTo(c => c.ContractStartDate.Year)
                    .WithMessage("Şirket kuruluş tarihi, kontrat başlangıç tarihinden önce olmalıdır.");

            RuleFor(c=>c.TaxOffice)
                 .NotEmpty()
                    .WithMessage("Lütfen vergi dairesinin ismini girin.")
                .NotNull()
                    .WithMessage("Lütfen vergi dairesinin ismini girin.")
                .Matches("^[a-zA-ZçğıöşüÇĞİÖŞÜ\\s]+$")
                    .WithMessage("Lütfen vergi dairesinin ismini yalnızca harflerden oluşacak şekilde girin.")
                .MinimumLength(3)
                    .WithMessage("Vergi dairesinin ismi 3 ile 50 karakter arasında olmalıdır.")
                .MaximumLength(50)
                    .WithMessage("Departman ismi 3 ile 50 karakter arasında olmalıdır.");

            RuleFor(c => c.NumberOfEmployees)
                .NotEmpty()
                    .WithMessage("Lütfen çalışan kişi sayısını girin.")
                .NotNull()
                    .WithMessage("Lütfen çalışan kişi sayısını girin.")
                .GreaterThanOrEqualTo(1)
                    .WithMessage("Çalışan sayısı en az 1 olmak zorundadır.");

            RuleFor(c=>c.Logo)
                .NotEmpty()
                    .WithMessage("Lütfen şirket logonuzu yükleyin.")
                .NotNull()
                    .WithMessage("Lütfen şirket logonuzu yükleyin.")
                .ChildRules(photoFile =>
                {
                    photoFile.RuleFor(p => p.FileName)
                    .NotEmpty()
                        .WithMessage("Lütfen şirket logonuzu yükleyin.")
                    .NotNull()
                        .WithMessage("Lütfen şirket logonuzu yükleyin.")
                    .Must(ExternalMethodsForValidators.IsPhotoExtensionValid)
                        .WithMessage("Lütfen şirket logonuzu uygun formatta yükleyin.");
                })
                .Must(ExternalMethodsForValidators.IsPhotoSizeValid)
                    .WithMessage("Logo dosya boyutu maksimum 25MB olmalıdır.");

            RuleFor(c=>c.Name)
                .NotEmpty()
                    .WithMessage("Lütfen şirket isminizi girin.")
                .NotNull()
                    .WithMessage("Lütfen şirket isminizi girin.")
                .MinimumLength(1)
                    .WithMessage("Şirket ismi 1 ile 50 karakter arasında olmalıdır")
                .MaximumLength(50)
                    .WithMessage("Şirket ismi 1 ile 50 karakter arasında olmalıdır")
                .Matches("^[a-zA-ZçğıöşüÇĞİÖŞÜ0-9&+\\-.\\s]+$")
                    .WithMessage("Lütfen şirket isminizi doğru formatta girin.");

            RuleFor(c => c.PhoneNumber)
              .NotEmpty()
                  .WithMessage("Lütfen şirketinize ait telefon numarasını girin.")
              .NotNull()
                  .WithMessage("Lütfen şirketinize ait telefon numarasını girin.")
              .Length(11)
                  .WithMessage("Telefon numarası 11 haneli olmalıdır. Örnek: 05051234567")
              .Matches("^[0-9]+$")
                  .WithMessage("Lütfen şirket telefon numarasını yalnızca sayılardan oluşacak şekilde girin.");

        }
    }
}
