using AutoMapper;
using IK.Domain.Entities.Identity;
using IKProject.Application.Features.Commands.RegisterCompany;
using IKProject.Application.Features.Querries.GetUser;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Application.Interfaces.Token;
using IKProject.Application.Methods.Get;
using IKProject.Application.Methods.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Commands.SetManagerToCompany
{
    public class SetManagerCommandHandler : IRequestHandler<SetManagerCommand, Unit>
    {
        private readonly ICompanyWriteRepository _CompanyWriteRepository;
        private readonly ICompanyReadRepository _CompanyReadRepository;
        private readonly ITokenServices _tokenServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly GetHelper _getHelper;
      


        public SetManagerCommandHandler(ICompanyWriteRepository CompanyWriteRepository, ITokenServices tokenServices, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, ICompanyReadRepository companyReadRepository, IUserWriteRepository userWriteRepository, GetHelper getHelper)
        {
            _CompanyWriteRepository = CompanyWriteRepository;
            _tokenServices = tokenServices;
            _httpContextAccessor = httpContextAccessor;
            _CompanyReadRepository = companyReadRepository;
            _userWriteRepository = userWriteRepository;
            _userManager = userManager;
            _getHelper = getHelper;
        }

        public async Task<Unit> Handle(SetManagerCommand request, CancellationToken cancellationToken)
        {
            var principal = RegisterHelpers.ValidateToken(_tokenServices, _httpContextAccessor);
            _getHelper.EnsureUserIsAdmin(principal);

            var manager = await _userManager.FindByEmailAsync(request.UserEmail);
            if (manager == null)
                throw new Exception("Yönetici bulunamadı.");

            var company = await _CompanyReadRepository.GetSingleAsync(c => c.Email == request.CompanyEmail);
            if (company == null)
                throw new Exception("Şirket bulunamadı.");


            company.AppUser.Add(manager);
            manager.CompanyId = company.Id;

            _CompanyWriteRepository.Update(company);
            await _userManager.UpdateAsync(manager);

            await _CompanyWriteRepository.SaveAsync();
            await _userWriteRepository.SaveAsync();


            return Unit.Value;
        }
    }
}
