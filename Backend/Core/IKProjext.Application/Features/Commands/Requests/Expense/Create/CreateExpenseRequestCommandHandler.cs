using AutoMapper;
using IK.Domain.Entities.Abstract;
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

namespace IKProject.Application.Features.Commands.Requests.Expense.Create
{
    public class CreateExpenseRequestCommandHandler : IRequestHandler<CreateExpenseRequestCommand, Unit>
    {
        private readonly IExpenseRequestWriteRepository _expenseRequestWriteRepository;
        private readonly ITokenServices _tokenServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateExpenseRequestCommandHandler(
            IExpenseRequestWriteRepository expenseRequestWriteRepository,
            ITokenServices tokenServices,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _expenseRequestWriteRepository = expenseRequestWriteRepository;
            _tokenServices = tokenServices;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateExpenseRequestCommand request, CancellationToken cancellationToken)
        {
            var principal = RequestHelpers.ValidateToken(_tokenServices, _httpContextAccessor);
            var userId = RequestHelpers.GetUserId(principal);

            byte[] documentContent = RequestHelpers.ProcessDocumentFile(request.Document);

            var expenseRequest = _mapper.Map<IK.Domain.Entities.Concrete.ExpenseRequest>(request);
            expenseRequest.Id = Guid.NewGuid();
            expenseRequest.Document = documentContent;
            expenseRequest.UserId = Guid.Parse(userId);
            expenseRequest.RequestStatus = RequestStatus.Beklemede;

            var appUserExpenseRequest = new AppUserExpenseRequest
            {
                AppUserId = Guid.Parse(userId),
                ExpenseRequestId = expenseRequest.Id
            };

            var appUserExpenseRequestResult = await _expenseRequestWriteRepository.AddAppUserExpenseRequest(appUserExpenseRequest);
            var addResult = await _expenseRequestWriteRepository.AddAsync(expenseRequest);

            if (!addResult || !appUserExpenseRequestResult)
                throw new Exception("Expense request creation failed.");

            await _expenseRequestWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
