using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Commands.Update
{
    public class UpdateCommand : IRequest<Unit>
    {
        public string PhoneNumber { get; set; }
        public IFormFile? PhotoFile { get; set; }
        public string Address { get; set; }

    }
}
