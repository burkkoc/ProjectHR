using IK.Domain.Enums;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace IKProject.Application.Features.Commands.Requests.Advance.Create
{
    public class CreateAdvanceRequestCommand : IRequest<Unit>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdvanceType? AdvanceType { get; set; }
        public decimal AdvanceAmount { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Currency AdvanceCurrency { get; set; }
        public string? Description { get; set; }
        //public DateTime RequestDate { get; set; }
    }
}