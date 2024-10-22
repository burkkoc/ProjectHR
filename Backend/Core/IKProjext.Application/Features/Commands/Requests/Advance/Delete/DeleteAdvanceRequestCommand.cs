using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Commands.Requests.Advance.Delete
{
    public class DeleteAdvanceRequestCommand : IRequest<Unit>
    {
        public Guid RequestId { get; set; }
    }
}
