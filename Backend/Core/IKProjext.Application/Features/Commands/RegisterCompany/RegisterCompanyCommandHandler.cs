using IK.Domain.Entities.Concrete;
using IKProject.Application.Interfaces.Repositories;
using IKProject.Application.Interfaces.Token;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using IKProject.Application.Methods.Register;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Application.Methods.Get;
using AutoMapper;

namespace IKProject.Application.Features.Commands.RegisterCompany
{
    public class RegisterCompanyCommandHandler : IRequestHandler<RegisterCompanyCommand, Unit>
    {
        private readonly ICompanyWriteRepository _CompanyWriteRepository;
        private readonly ITokenServices _tokenServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GetHelper _getHelper;
        private readonly IMapper _mapper;

        public RegisterCompanyCommandHandler(ICompanyWriteRepository CompanyWriteRepository, ITokenServices tokenServices, IHttpContextAccessor httpContextAccessor, GetHelper getHelper,IMapper mapper)
        {
            _CompanyWriteRepository = CompanyWriteRepository;
            _tokenServices = tokenServices;
            _httpContextAccessor = httpContextAccessor;
            _getHelper = getHelper;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(RegisterCompanyCommand request, CancellationToken cancellationToken)
        {
            var principal = RegisterHelpers.ValidateToken(_tokenServices, _httpContextAccessor);
            _getHelper.EnsureUserIsAdmin(principal);

            byte[] companyLogo = RegisterHelpers.ProcessPhotoFile(request.Logo);

              var company=_mapper.Map<Company>(request);
              company.Id=Guid.NewGuid();
              company.Logo=companyLogo;
              company.IsActive=true;
              company.Added=DateTime.Now;

            var result = await _CompanyWriteRepository.AddAsync(company);

            if (!result)
                throw new Exception("Company creation failed.");

            await _CompanyWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
