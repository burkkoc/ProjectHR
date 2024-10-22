using IKProject.Application.Features.PasswordReset;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Application.Interfaces.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IKProject.Infrastructure.GeneralServices
{
    public class RandomPasswordService : IRandomPasswordService
    {
        private readonly IMailService _mailService;

        public RandomPasswordService(IMailService mailService)
        {
            _mailService = mailService;
        }

        public string GenerateRandomPassword()
        {
            var random = new Random();
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%*_-+=<>?";

            char[] password =
            [
                uppercase[random.Next(uppercase.Length)],
                uppercase[random.Next(uppercase.Length)],
                lowercase[random.Next(lowercase.Length)],
                lowercase[random.Next(lowercase.Length)],
                digits[random.Next(digits.Length)],
                digits[random.Next(digits.Length)],
                special[random.Next(special.Length)],
                special[random.Next(special.Length)],
            ];
            return new string(password.OrderBy(x => random.Next()).ToArray());
        }

        public async Task<bool> SendPasswordToEmailAsync(string email, string password)
        {
            await _mailService.SendRandomPasswordAsync(email, password);
            return true;
        }
    }
}
