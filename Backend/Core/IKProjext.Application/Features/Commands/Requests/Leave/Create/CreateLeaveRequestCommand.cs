using IK.Domain.Enums;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace IKProject.Application.Features.Commands.Requests.Leave.Create
{
    public class CreateLeaveRequestCommand : IRequest<Unit>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LeaveType LeaveType { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public int DaysCount { get; set; }
        //public DateTime RequestDate { get; set; }
    }
}
