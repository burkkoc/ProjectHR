using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.PasswordReset
{
   
        public class PasswordSend : IRequest<PasswordResetCommandResponse>
        {
            public string Email { get; set; }
        }
    }

