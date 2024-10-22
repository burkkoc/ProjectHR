using AutoMapper;
using IK.Domain.Entities.Concrete;
using IK.Domain.Entities.Identity;
using IKProject.Application.DTOs.RequestDTOs;
using IKProject.Application.Interfaces.Repositories.RequestRepos;
using IKProject.Application.Methods.Get;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.GetAllRequests
{
    public class GetAllRequestsQueryHandler : IRequestHandler<GetAllRequestsQuery, GetAllRequestsQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAdvanceRequestReadRepository _advanceRequestReadRepository;
        private readonly IExpenseRequestReadRepository _expenseRequestReadRepository;
        private readonly ILeaveRequestReadRepository _leaveRequestReadRepository;
        private readonly GetHelper _getHelper;
        private readonly UserManager<AppUser> _userManager;

        public GetAllRequestsQueryHandler(IMapper mapper,
                                          IAdvanceRequestReadRepository advanceRequestReadRepository,
                                          IExpenseRequestReadRepository expenseRequestReadRepository,
                                          ILeaveRequestReadRepository leaveRequestReadRepository,
                                          GetHelper getHelper,
                                          UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _advanceRequestReadRepository = advanceRequestReadRepository;
            _expenseRequestReadRepository = expenseRequestReadRepository;
            _leaveRequestReadRepository = leaveRequestReadRepository;
            _getHelper = getHelper;
            _userManager = userManager;
        }

        public async Task<GetAllRequestsQueryResponse> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var principal = _getHelper.GetPrincipalFromToken(request.Token);
                var userId = _getHelper.GetUserId(principal);
                var queryingUser = await _userManager.FindByIdAsync(userId);
                var isManager = principal.IsInRole("manager");

                List<AdvanceRequest> advanceRequests;
                List<ExpenseRequest> expenseRequests;
                List<LeaveRequest> leaveRequests;

                if (isManager)
                {
                    advanceRequests = await _advanceRequestReadRepository.GetAll()
                        .Where(ar => ar.AppUser.CompanyId == queryingUser.CompanyId)
                        .ToListAsync(cancellationToken);

                    expenseRequests = await _expenseRequestReadRepository.GetAll()
                        .Where(er => er.AppUser.CompanyId == queryingUser.CompanyId)
                        .ToListAsync();

                    leaveRequests = await _leaveRequestReadRepository.GetAll()
                        .Where(lr => lr.AppUser.CompanyId == queryingUser.CompanyId)
                        .ToListAsync(cancellationToken);
                }
                else
                {
                    var userGuid = new Guid(userId);

                    advanceRequests = await _advanceRequestReadRepository.GetAll()
                        .Where(ar => ar.AppUser.CompanyId == queryingUser.CompanyId && ar.UserId == userGuid)
                        .ToListAsync(cancellationToken);

                    expenseRequests = await _expenseRequestReadRepository.GetAll()
                        .Where(er => er.AppUser.CompanyId == queryingUser.CompanyId && er.UserId == userGuid)
                        .ToListAsync(cancellationToken);

                    leaveRequests = await _leaveRequestReadRepository.GetAll()
                        .Where(lr => lr.AppUser.CompanyId == queryingUser.CompanyId && lr.UserId == userGuid)
                        .ToListAsync(cancellationToken);
                }

                var expenseRequestDTOs = _mapper.Map<IEnumerable<ExpenseRequest>, IEnumerable<ExpenseRequestDTO>>(expenseRequests);
                var advanceRequestDTOs = _mapper.Map<IEnumerable<AdvanceRequest>, IEnumerable<AdvanceRequestDTO>>(advanceRequests);
                var leaveRequestDTOs = _mapper.Map<IEnumerable<LeaveRequest>, IEnumerable<LeaveRequestDTO>>(leaveRequests);


                var response = new GetAllRequestsQueryResponse
                {
                    ExpenseRequests = expenseRequestDTOs.ToList(),
                    AdvanceRequests = advanceRequestDTOs.ToList(),
                    LeaveRequests = leaveRequestDTOs.ToList()

                };

                return response;
            }
            catch (UnauthorizedAccessException uae)
            {
                throw new UnauthorizedAccessException($"Yetki hatası: {uae.Message}", uae);
            }
            catch (Exception ex)
            {
                throw new Exception($"İstekler getirilirken hata oluştu: {ex.Message}", ex);
            }
        }
    }
}
