using AutoMapper;
using IK.Domain.Entities.Identity;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Application.Interfaces.Token;
using IKProject.Application.Methods.Get;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.GetAllManagers
{
    public class GetAllManagersQueryHandler : IRequestHandler<GetAllManagersQuery, List<GetAllManagersQueryResponse>>
    {
        private readonly GetHelper _getHelper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICompanyReadRepository _companyReadRepository;
        public GetAllManagersQueryHandler(GetHelper getHelper, UserManager<AppUser> userManager, IMapper mapper, ICompanyReadRepository companyReadRepository)
        {
            _getHelper = getHelper;
            _userManager = userManager;
            _mapper = mapper;
            _companyReadRepository = companyReadRepository;
        }

        public async Task<List<GetAllManagersQueryResponse>> Handle(GetAllManagersQuery request, CancellationToken cancellationToken)
        {
            var principal = _getHelper.GetPrincipalFromToken(request.Token);
            _getHelper.EnsureUserIsAdmin(principal);

            var managers = await _userManager.GetUsersInRoleAsync("manager");

            List<GetAllManagersQueryResponse> listOfManagers = new List<GetAllManagersQueryResponse>();
            foreach (var manager in managers)
            {
                var managerResponse = _mapper.Map<GetAllManagersQueryResponse>(manager);
                if (manager.CompanyId != null)
                {
                    var comp = await _companyReadRepository.GetByIdAsync(manager.CompanyId.ToString());
                    managerResponse.CompanyEmail = comp.Email;
                }
                else
                    managerResponse.CompanyEmail = string.Empty;
                
                listOfManagers.Add(managerResponse);
            }
            return listOfManagers;
        }
    }
}
