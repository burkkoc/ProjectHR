using MediatR;

namespace IKProject.Application.Features.PasswordReset
{
    public class PasswordResetCommandRequest : IRequest<PasswordResetCommandResponse>
    {
        public string Email { get; set; }
        public string? VerificationCode { get; set; }
        public string NewPassword { get; set; }

        public bool MustChangePassword { get; set; }
    }
}