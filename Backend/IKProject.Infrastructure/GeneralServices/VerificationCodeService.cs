using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using IK.Domain.Entities.Abstract;
using IKProject.Application.Features.PasswordReset;
using IKProject.Application.Interfaces.Repositories;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Application.Interfaces.Services;

namespace IKProject.Infrastructure.Services
{
    public class VerificationCodeService : IVerificationCodeService
    {
        private readonly IMailService _mailService;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
       
        public VerificationCodeService(IMailService mailService, IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository)
        {
            _mailService = mailService;
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task<string> GenerateVerificationCode(PasswordSend email)
        {
            var code = new Random().Next(100000, 999999).ToString(); // 6 haneli kod oluştur
            var expiration = DateTime.UtcNow.AddMinutes(15); // Kodun geçerlilik süresi 15 dakika
            await _mailService.SendVerificationCodeAsync(email.Email, code);
            var user=await _userReadRepository.GetSingleAsync(u => u.AppUser.Email==email.Email);
            user.AppUser.VerificationCode = code;
            user.AppUser.VerificationCodeExpiration = expiration;
            _userWriteRepository.Update(user);
            await _userWriteRepository.SaveAsync();
            return code;
        }


        public async Task<bool> ValidateVerificationCode(string email, string code)
        {
            var user = await _userReadRepository.GetSingleAsync(u => u.AppUser.Email == email);
            if (user.AppUser.VerificationCode==code && user.AppUser.VerificationCodeExpiration>DateTime.UtcNow)
            {
                return true;
            }
            return (false);
        }
    }
}
