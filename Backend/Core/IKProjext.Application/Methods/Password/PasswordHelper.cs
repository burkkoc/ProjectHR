using IK.Domain.Entities.Identity;
using IKProject.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IKProject.Application.Methods.PasswordReset
{
    public class PasswordHelper
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly ILogger<PasswordHelper> _logger;

        public PasswordHelper(UserManager<AppUser> userManager, IVerificationCodeService verificationCodeService, ILogger<PasswordHelper> logger)
        {
            _userManager = userManager;
            _verificationCodeService = verificationCodeService;
            _logger = logger;
        }

        public async Task<(bool success, string message)> ResetPasswordAsync(string email, string verificationCode, string newPassword, bool mustChangePassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogError($"User not found for email {email}");
                return (false, "User not found.");
            }

            if (mustChangePassword != false)
            {
                if (!await _verificationCodeService.ValidateVerificationCode(email, verificationCode))
                {
                    _logger.LogError($"Invalid verification code for email {email}");
                    return (false, "Invalid verification code.");
                }
            }
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors);
                _logger.LogError($"Password reset failed for email {email}: {errors}");
                return (false, "Password reset failed: " + errors);
            }

            if (user.MustChangePassword != true)
            {
                user.MustChangePassword = true;
                await _userManager.UpdateAsync(user);
            }

            _logger.LogInformation($"Password reset successful for email {email}");
            return (true, "Password reset successful.");
        }
    }
}
