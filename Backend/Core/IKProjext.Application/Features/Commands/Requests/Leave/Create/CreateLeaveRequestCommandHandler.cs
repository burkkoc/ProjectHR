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

namespace IKProject.Application.Features.Commands.Requests.Leave.Create
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestWriteRepository _leaveRequestWriteRepository;
        private readonly ITokenServices _tokenServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(
            ILeaveRequestWriteRepository leaveRequestWriteRepository,
            ITokenServices tokenServices,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _leaveRequestWriteRepository = leaveRequestWriteRepository;
            _tokenServices = tokenServices;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var principal = RequestHelpers.ValidateToken(_tokenServices, _httpContextAccessor);
            var userId = RequestHelpers.GetUserId(principal);

            var leaveRequest = _mapper.Map<LeaveRequest>(request);
            leaveRequest.Id = Guid.NewGuid();
            leaveRequest.UserId = Guid.Parse(userId);
            leaveRequest.RequestStatus = RequestStatus.Beklemede;

            var appUserLeaveRequest = new AppUserLeaveRequest
            {
                AppUserId = Guid.Parse(userId),
                LeaveRequestId = leaveRequest.Id
            };

            var appUserLeaveRequestResult = await _leaveRequestWriteRepository.AddAppUserLeaveRequest(appUserLeaveRequest);
            var addResult = await _leaveRequestWriteRepository.AddAsync(leaveRequest);

            if (!addResult || !appUserLeaveRequestResult)
                throw new Exception("Leave request creation failed.");

            await _leaveRequestWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
