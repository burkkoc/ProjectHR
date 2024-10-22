using AutoMapper;
using IK.Domain.Entities.Identity;
using IKProject.Application.Features.Querries.GetCompany;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Application.Interfaces.Token;
using IKProject.Application.Methods.Update;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.GetSingleCompany
{
    public class GetSingleCompanyQueryHandler : IRequestHandler<GetSingleCompanyQuery, GetSingleCompanyQueryResponse>
    {
        private readonly ITokenServices _tokenServices;
        private readonly ICompanyReadRepository _companyReadRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;


        public GetSingleCompanyQueryHandler(ITokenServices tokenServices, ICompanyReadRepository companyReadRepository, IMapper mapper, UserManager<AppUser> userManager)
        {
            _tokenServices = tokenServices;
            _companyReadRepository = companyReadRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<GetSingleCompanyQueryResponse> Handle(GetSingleCompanyQuery request, CancellationToken cancellationToken)
        {
            var principal = _tokenServices.GetClaimsPrincipalFromExpiredToken(request.Token);
            if (principal == null)
                throw new Exception("Geçersiz token");

            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                throw new Exception("Kullanıcı bulunamadı");

            var user = await _userManager.FindByIdAsync(userId);
            
            //var company = await _companyReadRepository.GetSingleAsync(c => c.AppUserId == Guid.Parse(userId));
            if (user.CompanyId == null)
                throw new Exception("Şirket bulunamadı.");

            

            return _mapper.Map<GetSingleCompanyQueryResponse>(user.Company);
        }
    }
}
