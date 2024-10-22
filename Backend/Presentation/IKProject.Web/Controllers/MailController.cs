
using IKProject.Application.Features.PasswordReset;
using IKProject.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IKProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly ILogger<MailController> _logger;

        public MailController(IMediator mediator, IVerificationCodeService verificationCodeService, ILogger<MailController> logger)
        {
            _mediator = mediator;
            _verificationCodeService = verificationCodeService;
            _logger = logger;
        }

        [HttpPost("send-verification-code")]
        public async Task<IActionResult> SendVerificationCode([FromBody] PasswordSend email)
        {
            try
            {
                var code = await _verificationCodeService.GenerateVerificationCode(email);
                return Ok(new { message = "Verification code sent", code });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send verification code to email {email}");
                return BadRequest(new { message = "Failed to send verification code", error = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetCommandRequest command)
        {
            try
            {
                var response = await _mediator.Send(command);
                if (response.Success)
                {
                    return Ok(new { message = response.Message });
                }
                return BadRequest(new { message = response.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Password reset failed for email {command.Email}");
                return BadRequest(new { message = "Password reset failed", error = ex.Message });
            }
        }
    }
}
