using IK.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Commands.ApproveOrReject
{
    public class ApproveOrRejectRequestCommand : IRequest<Unit>
    {
        public Guid RequestId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RequestStatus RequestStatus { get; set; }
    }
}
