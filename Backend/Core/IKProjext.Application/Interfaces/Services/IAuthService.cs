using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task PasswordResetAsync(string email);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
    }
}
