using IKProject.Application.Methods.PasswordReset;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IKProject.Application.Features.PasswordReset
{
    public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, PasswordResetCommandResponse>
    {
        private readonly PasswordHelper _passwordHelper;

        public PasswordResetCommandHandler(PasswordHelper passwordHelper)
        {
            _passwordHelper = passwordHelper;
        }

        public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
        {
            var (success, message) = await _passwordHelper.ResetPasswordAsync(request.Email, request.VerificationCode, request.NewPassword, request.MustChangePassword);
            return new PasswordResetCommandResponse { Success = success, Message = message };
        }
    }
}
