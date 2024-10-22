using IKProject.Application.Features.PasswordReset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Interfaces.Services
{
    public interface IVerificationCodeService
    {
        Task<string> GenerateVerificationCode(PasswordSend email);
        Task<bool> ValidateVerificationCode(string email, string code);
    }
}
