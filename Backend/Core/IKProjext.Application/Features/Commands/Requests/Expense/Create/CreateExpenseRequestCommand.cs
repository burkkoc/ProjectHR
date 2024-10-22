using IK.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json.Serialization;

namespace IKProject.Application.Features.Commands.Requests.Expense.Create
{
    public class CreateExpenseRequestCommand : IRequest<Unit>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ExpenseType ExpenseType { get; set; }
        public decimal ExpenseAmount { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Currency Currency { get; set; }
        public IFormFile Document { get; set; }
        //public DateTime RequestDate { get; set; }
    }
}
