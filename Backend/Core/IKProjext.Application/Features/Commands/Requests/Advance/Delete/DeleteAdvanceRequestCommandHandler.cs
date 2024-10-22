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

namespace IKProject.Application.Features.Commands.Requests.Advance.Delete
{
    public class DeleteAdvanceRequestCommandHandler : IRequestHandler<DeleteAdvanceRequestCommand, Unit>
    {
        private readonly IAdvanceRequestReadRepository _advanceRequestReadRepository;
        private readonly IAdvanceRequestWriteRepository _advanceRequestWriteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenServices _tokenServices;

        public DeleteAdvanceRequestCommandHandler(
            IAdvanceRequestReadRepository advanceRequestReadRepository,
            IAdvanceRequestWriteRepository advanceRequestWriteRepository,
            IHttpContextAccessor httpContextAccessor,
            ITokenServices tokenServices)
        {
            _advanceRequestReadRepository = advanceRequestReadRepository;
            _advanceRequestWriteRepository = advanceRequestWriteRepository;
            _httpContextAccessor = httpContextAccessor;
            _tokenServices = tokenServices;
        }

        public async Task<Unit> Handle(DeleteAdvanceRequestCommand request, CancellationToken cancellationToken)
        {
            var token = RequestHelpers.GetTokenFromHeader(_httpContextAccessor);
            var principal = RequestHelpers.GetPrincipalFromToken(token, _tokenServices);
            var userId = RequestHelpers.GetUserId(principal);
            var userGuid = Guid.Parse(userId);

            if (!principal.IsInRole("employee"))
            {
                throw new UnauthorizedAccessException("Bu işlemi yapma yetkiniz yok.");
            }

            var advanceRequest = await _advanceRequestReadRepository.GetByIdAsync(request.RequestId.ToString());

            if (advanceRequest == null)
            {
                throw new Exception("Talep bulunamadı.");
            }

            if (advanceRequest.UserId != userGuid)
            {
                throw new UnauthorizedAccessException("Bu talebi silme yetkiniz yok.");
            }

            var removedRelatedRecords = await _advanceRequestWriteRepository.RemoveRelatedRecordsAsync(request.RequestId);
            if (!removedRelatedRecords)
            {
                throw new Exception("İlişkili kayıtlar silinemedi.");
            }

            var result = _advanceRequestWriteRepository.Remove(advanceRequest);
            if (!result)
            {
                throw new Exception("Talep silinemedi.");
            }
            await _advanceRequestWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
