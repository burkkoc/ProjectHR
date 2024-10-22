using IK.Domain.Enums;
using IKProject.Application.Interfaces.Repositories.RequestRepos;
using IKProject.Application.Interfaces.Token;
using IKProject.Application.Methods.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Commands.ApproveOrReject
{
    public class ApproveOrRejectRequestCommandHandler : IRequestHandler<ApproveOrRejectRequestCommand, Unit>
    {
        private readonly IAdvanceRequestReadRepository _advanceRequestReadRepository;
        private readonly IAdvanceRequestWriteRepository _advanceRequestWriteRepository;
        private readonly IExpenseRequestReadRepository _expenseRequestReadRepository;
        private readonly IExpenseRequestWriteRepository _expenseRequestWriteRepository;
        private readonly ILeaveRequestReadRepository _leaveRequestReadRepository;
        private readonly ILeaveRequestWriteRepository _leaveRequestWriteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenServices _tokenServices;

        public ApproveOrRejectRequestCommandHandler(
            IAdvanceRequestReadRepository advanceRequestReadRepository,
            IAdvanceRequestWriteRepository advanceRequestWriteRepository,
            IExpenseRequestReadRepository expenseRequestReadRepository,
            IExpenseRequestWriteRepository expenseRequestWriteRepository,
            ILeaveRequestReadRepository leaveRequestReadRepository,
            ILeaveRequestWriteRepository leaveRequestWriteRepository,
            IHttpContextAccessor httpContextAccessor,
            ITokenServices tokenServices)
        {
            _advanceRequestReadRepository = advanceRequestReadRepository;
            _advanceRequestWriteRepository = advanceRequestWriteRepository;
            _expenseRequestReadRepository = expenseRequestReadRepository;
            _expenseRequestWriteRepository = expenseRequestWriteRepository;
            _leaveRequestReadRepository = leaveRequestReadRepository;
            _leaveRequestWriteRepository = leaveRequestWriteRepository;
            _httpContextAccessor = httpContextAccessor;
            _tokenServices = tokenServices;
        }

        public async Task<Unit> Handle(ApproveOrRejectRequestCommand request, CancellationToken cancellationToken)
        {
            var token = RequestHelpers.GetTokenFromHeader(_httpContextAccessor);
            var principal = RequestHelpers.GetPrincipalFromToken(token, _tokenServices);

            if (!principal.IsInRole("manager"))
            {
                throw new UnauthorizedAccessException("Bu işlemi yapma yetkiniz yok.");
            }

            bool isProcessed = false;

            var advanceRequest = await _advanceRequestReadRepository.GetByIdAsync(request.RequestId.ToString());
            if (advanceRequest != null)
            {
                advanceRequest.RequestStatus = request.RequestStatus;
                advanceRequest.ResponseDate = DateTime.UtcNow;
                var result = _advanceRequestWriteRepository.Update(advanceRequest);
                if (!result)
                {
                    throw new Exception("Talep durumu güncellenemedi.");
                }
                await _advanceRequestWriteRepository.SaveAsync();
                isProcessed = true;
            }

            var expenseRequest = await _expenseRequestReadRepository.GetByIdAsync(request.RequestId.ToString());
            if (expenseRequest != null)
            {
                expenseRequest.RequestStatus = request.RequestStatus;
                expenseRequest.ResponseDate = DateTime.UtcNow;
                var result = _expenseRequestWriteRepository.Update(expenseRequest);
                if (!result)
                {
                    throw new Exception("Talep durumu güncellenemedi.");
                }
                await _expenseRequestWriteRepository.SaveAsync();
                isProcessed = true;
            }

            var leaveRequest = await _leaveRequestReadRepository.GetByIdAsync(request.RequestId.ToString());
            if (leaveRequest != null)
            {
                leaveRequest.RequestStatus = request.RequestStatus;
                leaveRequest.ResponseDate = DateTime.UtcNow;
                var result = _leaveRequestWriteRepository.Update(leaveRequest);
                if (!result)
                {
                    throw new Exception("Talep durumu güncellenemedi.");
                }
                await _leaveRequestWriteRepository.SaveAsync();
                isProcessed = true;
            }

            if (!isProcessed)
            {
                throw new Exception("Talep bulunamadı.");
            }

            return Unit.Value;
        }
    }
}
