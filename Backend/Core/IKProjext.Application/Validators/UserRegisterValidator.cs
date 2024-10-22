using FluentValidation;
using IKProject.Application.Features.Commands.Register;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IKProject.Application.Validators
{
    public class UserRegisterValidator : AbstractValidator<RegisterCommand>
    {
        private readonly ICompanyReadRepository _companyReadRepository;
        public UserRegisterValidator(ICompanyReadRepository companyReadRepository)
        {
            _companyReadRepository = companyReadRepository;

            RuleFor(c => c.FirstName)
                .NotEmpty()
                    .WithMessage("Lütfen isminizi girin.")
                .NotNull()
                    .WithMessage("Lütfen isminizi girin.")
                .MinimumLength(2)
                    .WithMessage("İsim 2 ile 25 karakter arasında olmalıdır")
                .MaximumLength(25)
                    .WithMessage("İsim 2 ile 25 karakter arasında olmalıdır")
                .Matches("^[a-zA-ZçğıöşüÇĞİÖŞÜ]+$")
                .WithMessage("Lütfen isminizi yalnıca harflerden oluşacak şekilde girin.");


            RuleFor(c => c.LastName)
                .NotEmpty()
                    .WithMessage("Lütfen soyadınızı girin.")
                .NotNull()
                    .WithMessage("Lütfen soyadınızı girin.")
                .MinimumLength(2)
                    .WithMessage("Soyad 2 ile 25 karakter arasında olmalıdır")
                .MaximumLength(25)
                    .WithMessage("Soyad 2 ile 25 karakter arasında olmalıdır")
                .Matches("^[a-zA-ZçğıöşüÇĞİÖŞÜ]+$")
                    .WithMessage("Lütfen soyadınızı yalnızca harflerden oluşacak şekilde girin.");

            RuleFor(c => c.SecondLastName)
                .MinimumLength(2)
                    .WithMessage("İkinci soyad 2 ile 25 karakter arasında olmalıdır")
                .MaximumLength(25)
                    .WithMessage("İkinci soyad 2 ile 25 karakter arasında olmalıdır")
                .Matches("^[a-zA-ZçğıöşüÇĞİÖŞÜ]+$")
                    .WithMessage("Lütfen ikinci soyadınızı yalnızca harflerden oluşacak şekilde girin.");

            RuleFor(c => c.SecondName)
                .MinimumLength(2)
                    .WithMessage("İkinci ad 2 ile 25 karakter arasında olmalıdır")
                .MaximumLength(25)
                    .WithMessage("İkinci ad 2 ile 25 karakter arasında olmalıdır")
                .Matches("^[a-zA-ZçğıöşüÇĞİÖŞÜ]+$")
                    .WithMessage("Lütfen ikinci adınızı yalnızca harflerden oluşacak şekilde girin.");

            RuleFor(c => c.PhoneNumber)
                .NotEmpty()
                    .WithMessage("Lütfen telefon numaranızı girin.")
                .NotNull()
                    .WithMessage("Lütfen telefon numaranızı girin.")
                .Length(11)
                    .WithMessage("Telefon numarası 11 haneli olmalıdır. Örnek: 05051234567")
                .Matches("^[0-9]+$")
                    .WithMessage("Lütfen telefon numaranızı yalnızca sayılardan oluşacak şekilde girin.");

            RuleFor(c => c.TC)
                .NotEmpty()
                    .WithMessage("Lütfen TC kimlik numaranızı girin.")
                .NotNull()
                    .WithMessage("Lütfen TC kimlik numaranızı girin.")
                .Length(11)
                    .WithMessage("TC kimlik numarası 11 haneli olmalıdır. Örnek: 12345678910")
                .Matches("^[0-9]+$")
                    .WithMessage("Lütfen TC kimlik numaranızı yalnızca sayılardan oluşacak şekilde girin.")
                .Must(ExternalMethodsForValidators.IsTCValid)
                    .WithMessage("Lütfen geçerli bir TC kimlik numarası girin.");


            RuleFor(c => c.PhotoFile)
                .NotEmpty()
                    .WithMessage("Lütfen fotoğrafınızı yükleyin.")
                .NotNull()
                    .WithMessage("Lütfen fotoğrafınızı yükleyin.")
                .ChildRules(photoFile =>
                {
                    photoFile.RuleFor(p => p.FileName)
                    .NotEmpty()
                        .WithMessage("Lütfen fotoğrafınızı yükleyin.")
                    .NotNull()
                        .WithMessage("Lütfen fotoğrafınızı yükleyin.")
                    .Must(ExternalMethodsForValidators.IsPhotoExtensionValid)
                        .WithMessage("Lütfen fotoğrafınızı uygun formatta yükleyin.");
                })
                .Must(ExternalMethodsForValidators.IsPhotoSizeValid)
                    .WithMessage("Fotoğraf büyüklüğü maksimum 25MB olmalıdır.");

            RuleFor(c => c.Profession)
                .NotEmpty()
                    .WithMessage("Lütfen mesleğinizi girin.")
                .NotNull()
                    .WithMessage("Lütfen mesleğinizi girin.")
                .Matches("^[a-zA-ZçğıöşüÇĞİÖŞÜ\\s]+$")
                    .WithMessage("Lütfen soyadınızı yalnızca harflerden oluşacak şekilde girin.")
                .MinimumLength(3)
                    .WithMessage("Mesleğiniz 3 ile 25 karakter arasında olmalıdır.")
                .MaximumLength(25)
                    .WithMessage("Mesleğiniz 3 ile 25 karakter arasında olmalıdır.");

            RuleFor(c => c.Department)
                .NotEmpty()
                    .WithMessage("Lütfen departman ismini girin.")
                .NotNull()
                    .WithMessage("Lütfen departman ismini girin.")
                .Matches("^[a-zA-ZçğıöşüÇĞİÖŞÜ\\s]+$")
                    .WithMessage("Lütfen departman ismini yalnızca harflerden oluşacak şekilde girin.")
                .MinimumLength(3)
                    .WithMessage("Departman ismi 3 ile 25 karakter arasında olmalıdır.")
                .MaximumLength(25)
                    .WithMessage("Departman ismi 3 ile 25 karakter arasında olmalıdır.");

            RuleFor(c => c.Address)
                .NotEmpty()
                    .WithMessage("Lütfen adresinizi girin.")
                .NotNull()
                    .WithMessage("Lütfen adresinizi girin.")
                .MinimumLength(10)
                    .WithMessage("Adres 10 ile 250 karakter arasında olmalıdır.")
                .MaximumLength(250)
                    .WithMessage("Adres 10 ile 250 karakter arasında olmalıdır.");

            RuleFor(c => c.BirthDate)
                .NotEmpty()
                    .WithMessage("Lütfen doğum tarihinizi girin")
                .NotNull()
                    .WithMessage("Lütfen doğum tarihinizi girin")
                .Must(ExternalMethodsForValidators.IsDateValid)
                    .WithMessage("Lütfen geçerli bir doğum tarihi girin.")
                .Must(ExternalMethodsForValidators.IsOver18)
                    .WithMessage("Kayıt olabilmek için yaşınız 18'den büyük olmalıdır.");

            RuleFor(c => c.StartDate)
                .NotEmpty()
                    .WithMessage("Lütfen işe başlama tarihini girin")
                .NotNull()
                    .WithMessage("Lütfen işe başlama tarihini girin")
                .GreaterThanOrEqualTo(DateTime.UtcNow.AddYears(-100))
                    .WithMessage("Lütfen geçerli bir tarih girin.")
                //.LessThan(c => c.EndDate)
                //    .WithMessage("İşe başlama tarihi, çıkış tarihinden sonra olamaz.")
                .GreaterThanOrEqualTo(c => c.BirthDate.AddYears(18))
                    .WithMessage("İşe başlama tarihi tarihi doğum tarihinden en az 18 yıl sonra olmalıdır.");


            RuleFor(c => c.EndDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow.AddYears(-100))
                    .WithMessage("Lütfen geçerli bir tarih girin.")
                .GreaterThan(c => c.StartDate)
                    .WithMessage("İşten çıkış tarihi, başlama tarihinden önce olamaz.")
                .GreaterThanOrEqualTo(c => c.BirthDate.AddYears(18))
                    .WithMessage("İşten çıkış tarihi tarihi doğum tarihinden en az 18 yıl sonra olmalıdır.");


            RuleFor(c => c.PlaceOfBirth)
                .NotNull()
                    .WithMessage("Lütfen doğum yerinizi girin.")
                .NotEmpty()
                    .WithMessage("Lütfen doğum yerinizi girin.")
                .MinimumLength(3)
                    .WithMessage("Doğum yeriniz 3 ile 25 karakter arasında olmalıdır.")
                .MaximumLength(25)
                    .WithMessage("Doğum yeriniz 3 ile 25 karakter arasında olmalıdır.")
                .Matches("^[a-zA-ZçğıöşüÇĞİÖŞÜ\\s]+$")
                    .WithMessage("Lütfen doğum yerinizi yalnızca harflerden oluşacak şekilde girin.");

            RuleFor(c => c.Mail)
                .NotNull()
                    .WithMessage("Lütfen email adresinizi girin.")
                .NotEmpty()
                    .WithMessage("Lütfen email adresinizi girin.")
                .Must(ExternalMethodsForValidators.IsMailValid)
                    .WithMessage("Email adresiniz ornek@ornek.com/net/org/com.tr/edu.tr formatında olmalıdır.")
                .MinimumLength(5)
                    .WithMessage("Email adresiniz 5 ile 50 karakter arasında olmalıdır.")
                .MaximumLength(50)
                    .WithMessage("Email adresiniz 5 ile 50 karakter arasında olmalıdır.");
        }
    }
}
