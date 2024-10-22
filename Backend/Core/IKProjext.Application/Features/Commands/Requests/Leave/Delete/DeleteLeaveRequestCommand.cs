using MediatR;
using System;

namespace IKProject.Application.Features.Commands.Requests.Leave.Delete
{
    public class DeleteLeaveRequestCommand : IRequest<Unit>
    {
        public Guid RequestId { get; set; }
    }
}
