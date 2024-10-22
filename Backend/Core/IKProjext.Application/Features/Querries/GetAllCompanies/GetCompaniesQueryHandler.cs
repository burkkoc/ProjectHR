using AutoMapper;
using IK.Domain.Entities.Concrete;
using IK.Domain.Entities.Identity;
using IKProject.Application.DTOs.RequestDTOs;
using IKProject.Application.Features.Querries.GetAllManagers;
using IKProject.Application.Features.Querries.GetAllRequests;
using IKProject.Application.Features.Querries.GetUser;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Application.Interfaces.Token;
using IKProject.Application.Methods.Get;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.GetCompany
{
    public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, List<GetCompaniesQueryResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenServices _tokenServices;
        private readonly ICompanyReadRepository _companyReadRepository;
        private readonly GetHelper _getHelper;
        private readonly IMapper _mapper;
        public GetCompaniesQueryHandler(UserManager<AppUser> userManager, ITokenServices tokenServices, ICompanyReadRepository companyReadRepository, GetHelper getHelper, IMapper mapper)
        {
            _userManager = userManager;
            _tokenServices = tokenServices;
            _companyReadRepository = companyReadRepository;
            _getHelper = getHelper;
            _mapper = mapper;
        }

        public async Task<List<GetCompaniesQueryResponse>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            var principle = _getHelper.GetPrincipalFromToken(request.Token);
            _getHelper.EnsureUserIsAdmin(principle);
            var getCompaniesQueryResponse = new GetCompaniesQueryResponse();

            List<Company> tempList = new List<Company>();
            List<Company> companies = await _companyReadRepository.GetAll().ToListAsync(cancellationToken);
            tempList = companies;

            var response = _mapper.Map<IEnumerable<Company>, IEnumerable<GetCompaniesQueryResponse>>(companies);

            var users = tempList.SelectMany(x => x.AppUser).ToList();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "manager") && response.Any(x => x.Id == user.CompanyId))
                {
                    var manager = response.First(x => x.Id == user.CompanyId);
                    var managerInfos = $"{user.FirstName},{user.LastName},{user.Email}";
                    manager.Managers.Add(managerInfos);


                }
            }


            return response.ToList();
        }

       
        
    }
}
