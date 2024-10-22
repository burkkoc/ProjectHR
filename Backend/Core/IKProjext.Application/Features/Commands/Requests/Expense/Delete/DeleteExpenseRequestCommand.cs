using MediatR;
using System;

namespace IKProject.Application.Features.Commands.Requests.Expense.Delete
{
    public class DeleteExpenseRequestCommand : IRequest<Unit>
    {
        public Guid RequestId { get; set; }
    }
}
