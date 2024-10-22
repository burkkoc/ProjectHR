using AutoMapper;
using IK.Domain.Entities;
using IK.Domain.Entities.Concrete;
using IK.Domain.Enums;
using IKProject.Application.Interfaces.Repositories.RequestRepos;
using IKProject.Application.Interfaces.Token;
using IKProject.Application.Methods.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Commands.Requests.Advance.Create
{
    public class CreateAdvanceRequestCommandHandler : IRequestHandler<CreateAdvanceRequestCommand, Unit>
    {
        private readonly IAdvanceRequestWriteRepository _advanceRequestWriteRepository;
        private readonly ITokenServices _tokenServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateAdvanceRequestCommandHandler(
            IAdvanceRequestWriteRepository advanceRequestWriteRepository,
            ITokenServices tokenServices,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _advanceRequestWriteRepository = advanceRequestWriteRepository;
            _tokenServices = tokenServices;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateAdvanceRequestCommand request, CancellationToken cancellationToken)
        {
            if (request.AdvanceType == AdvanceType.corporate)
            {
                request.Description = string.Empty;
            }

            RequestHelpers.ValidateDescription(request.AdvanceType, request.Description);

            var principal = RequestHelpers.ValidateToken(_tokenServices, _httpContextAccessor);
            var userId = RequestHelpers.GetUserId(principal);

            var advanceRequest = _mapper.Map<AdvanceRequest>(request);
            advanceRequest.Id = Guid.NewGuid();
            advanceRequest.UserId = Guid.Parse(userId);
            advanceRequest.RequestStatus = RequestStatus.Beklemede;

            var appUserAdvanceRequest = new AppUserAdvanceRequest
            {
                AppUserId = Guid.Parse(userId),
                AdvanceRequestId = advanceRequest.Id
            };

            var appUserAdvanceRequestResult = await _advanceRequestWriteRepository.AddAppUserAdvanceRequest(appUserAdvanceRequest);
            var addResult = await _advanceRequestWriteRepository.AddAsync(advanceRequest);

            if (!addResult || !appUserAdvanceRequestResult)
                throw new Exception("Advance request creation failed.");

            await _advanceRequestWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}