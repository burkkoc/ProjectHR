using IK.Domain.Entities.Concrete;
using IK.Domain.Entities.Identity;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Application.Interfaces.Token;
using IKProject.Application.Methods.Update;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Commands.UpdateCompany
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanyReadRepository _companyReadRepository;
        private readonly ICompanyWriteRepository _companyWriteRepository;
        private readonly UpdateHelper _updateHelper;

        public UpdateCompanyCommandHandler(UserManager<AppUser> userManager, ICompanyReadRepository companyReadRepository, ICompanyWriteRepository companyWriteRepository, UpdateHelper updateHelper)
        {
            _userManager = userManager;
            _companyReadRepository = companyReadRepository;
            _companyWriteRepository = companyWriteRepository;
            _updateHelper = updateHelper;
        }

        public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var token = _updateHelper.GetToken();
            var principal = _updateHelper.GetPrincipalFromToken(token);
            var userId = _updateHelper.GetUserId(principal);

            var user = await _userManager.FindByIdAsync(userId);
            _updateHelper.EnsureUserIsAdmin(principal);

            var company = await _companyReadRepository.GetSingleAsync(c => c.MersisNo == request.MersisNo);

            company.Logo = await _updateHelper.ProcessFile(request.LogoFile);
            company.PhoneNumber = request.PhoneNumber;
            company.Address = request.Address;
            company.Updated = DateTime.Now;

            _companyWriteRepository.Update(company);
            await _companyWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
