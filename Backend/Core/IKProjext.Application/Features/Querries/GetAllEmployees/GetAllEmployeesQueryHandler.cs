using AutoMapper;
using IK.Domain.Entities.Identity;
using IKProject.Application.Features.Querries.GetAllManagers;
using IKProject.Application.Methods.Get;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.GetAllEmployees
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<GetAllEmployeesQueryResponse>>
    {
        private readonly GetHelper _getHelper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(GetHelper getHelper, UserManager<AppUser> userManager, IMapper mapper)
        {
            _getHelper = getHelper;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<GetAllEmployeesQueryResponse>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var principal = _getHelper.GetPrincipalFromToken(request.Token);
            List<GetAllEmployeesQueryResponse> listOfEmployees = new List<GetAllEmployeesQueryResponse>();
            var employees = await _userManager.GetUsersInRoleAsync("employee");
            if (principal.IsInRole("admin"))
            {
                foreach (var employee in employees)
                {
                    var employeeReponse = _mapper.Map<GetAllEmployeesQueryResponse>(employee);
                    listOfEmployees.Add(employeeReponse);
                }
                return listOfEmployees;
            }
            else if (principal.IsInRole("manager"))
            {
                var userId = _getHelper.GetUserId(principal);
                var userQuerying = await _userManager.FindByIdAsync(userId);
                var filteredEmployees = employees.Where(au => au.CompanyId == userQuerying.CompanyId);

                foreach (var employee in filteredEmployees)
                {
                    var employeeReponse = _mapper.Map<GetAllEmployeesQueryResponse>(employee);
                    listOfEmployees.Add(employeeReponse);
                }
                return listOfEmployees;
            }
            else
                throw new Exception("Yetkiniz yok.");
            
        }
    }

}

