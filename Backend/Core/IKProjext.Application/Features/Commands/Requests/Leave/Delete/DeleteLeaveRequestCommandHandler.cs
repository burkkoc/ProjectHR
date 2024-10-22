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

namespace IKProject.Application.Features.Commands.Requests.Leave.Delete
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestReadRepository _leaveRequestReadRepository;
        private readonly ILeaveRequestWriteRepository _leaveRequestWriteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenServices _tokenServices;

        public DeleteLeaveRequestCommandHandler(
            ILeaveRequestReadRepository leaveRequestReadRepository,
            ILeaveRequestWriteRepository leaveRequestWriteRepository,
            IHttpContextAccessor httpContextAccessor,
            ITokenServices tokenServices)
        {
            _leaveRequestReadRepository = leaveRequestReadRepository;
            _leaveRequestWriteRepository = leaveRequestWriteRepository;
            _httpContextAccessor = httpContextAccessor;
            _tokenServices = tokenServices;
        }

        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var token = RequestHelpers.GetTokenFromHeader(_httpContextAccessor);
            var principal = RequestHelpers.GetPrincipalFromToken(token, _tokenServices);
            var userId = RequestHelpers.GetUserId(principal);
            var userGuid = Guid.Parse(userId);

            if (!principal.IsInRole("employee"))
            {
                throw new UnauthorizedAccessException("Bu işlemi yapma yetkiniz yok.");
            }

            var leaveRequest = await _leaveRequestReadRepository.GetByIdAsync(request.RequestId.ToString());

            if (leaveRequest == null)
            {
                throw new Exception("Talep bulunamadı.");
            }

            if (leaveRequest.UserId != userGuid)
            {
                throw new UnauthorizedAccessException("Bu talebi silme yetkiniz yok.");
            }

            var removedRelatedRecords = await _leaveRequestWriteRepository.RemoveRelatedRecordsAsync(request.RequestId);
            if (!removedRelatedRecords)
            {
                throw new Exception("İlişkili kayıtlar silinemedi.");
            }

            var result = _leaveRequestWriteRepository.Remove(leaveRequest);
            if (!result)
            {
                throw new Exception("Talep silinemedi.");
            }
            await _leaveRequestWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
