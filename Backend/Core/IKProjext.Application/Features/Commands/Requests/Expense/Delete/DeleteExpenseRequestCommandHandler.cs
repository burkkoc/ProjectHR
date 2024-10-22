using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using IKProject.Application.Interfaces.Repositories.RequestRepos;
using IKProject.Application.Interfaces.Token;
using IKProject.Application.Methods.Request;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace IKProject.Application.Features.Commands.Requests.Expense.Delete
{
    public class DeleteExpenseRequestCommandHandler : IRequestHandler<DeleteExpenseRequestCommand, Unit>
    {
        private readonly IExpenseRequestReadRepository _expenseRequestReadRepository;
        private readonly IExpenseRequestWriteRepository _expenseRequestWriteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenServices _tokenServices;

        public DeleteExpenseRequestCommandHandler(
            IExpenseRequestReadRepository expenseRequestReadRepository,
            IExpenseRequestWriteRepository expenseRequestWriteRepository,
            IHttpContextAccessor httpContextAccessor,
            ITokenServices tokenServices)
        {
            _expenseRequestReadRepository = expenseRequestReadRepository;
            _expenseRequestWriteRepository = expenseRequestWriteRepository;
            _httpContextAccessor = httpContextAccessor;
            _tokenServices = tokenServices;
        }

        public async Task<Unit> Handle(DeleteExpenseRequestCommand request, CancellationToken cancellationToken)
        {
            var token = RequestHelpers.GetTokenFromHeader(_httpContextAccessor);
            var principal = RequestHelpers.GetPrincipalFromToken(token, _tokenServices);
            var userId = RequestHelpers.GetUserId(principal);
            var userGuid = Guid.Parse(userId);

            if (!principal.IsInRole("employee"))
            {
                throw new UnauthorizedAccessException("Bu işlemi yapma yetkiniz yok.");
            }

            var expenseRequest = await _expenseRequestReadRepository.GetByIdAsync(request.RequestId.ToString());

            if (expenseRequest == null)
            {
                throw new Exception("Talep bulunamadı.");
            }

            if (expenseRequest.UserId != userGuid)
            {
                throw new UnauthorizedAccessException("Bu talebi silme yetkiniz yok.");
            }

            var removedRelatedRecords = await _expenseRequestWriteRepository.RemoveRelatedRecordsAsync(request.RequestId);
            if (!removedRelatedRecords)
            {
                throw new Exception("İlişkili kayıtlar silinemedi.");
            }

            var result = _expenseRequestWriteRepository.Remove(expenseRequest);
            if (!result)
            {
                throw new Exception("Talep silinemedi.");
            }
            await _expenseRequestWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
