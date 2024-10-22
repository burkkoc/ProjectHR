using IKProject.Application.Features.PasswordReset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Interfaces.Services
{
    public interface IRandomPasswordService
    {
        string GenerateRandomPassword();
        Task<bool> SendPasswordToEmailAsync(string email, string password);
    }
}
