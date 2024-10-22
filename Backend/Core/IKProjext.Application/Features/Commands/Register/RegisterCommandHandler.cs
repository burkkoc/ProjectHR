using IK.Domain.Entities.Concrete;
using IK.Domain.Entities.Identity;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Application.Interfaces.Token;
using IKProject.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using IKProject.Application.Methods.Register;
using IKProject.Application.Features.PasswordReset;
using AutoMapper;

namespace IKProject.Application.Features.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly ITokenServices _tokenServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMailService _mailService;
        private readonly IRandomPasswordService _randomPasswordService;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(UserManager<AppUser> userManager, IUserWriteRepository userWriteRepository, ITokenServices tokenServices, IHttpContextAccessor httpContextAccessor, IMailService mailService, IRandomPasswordService randomPasswordService,IMapper mapper)
        {
            _userManager = userManager;
            _userWriteRepository = userWriteRepository;
            _tokenServices = tokenServices;
            _httpContextAccessor = httpContextAccessor;
            _mailService = mailService;
            _randomPasswordService = randomPasswordService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var principal = RegisterHelpers.ValidateToken(_tokenServices, _httpContextAccessor);
            var registererId = RegisterHelpers.GetUserId(principal);

            //byte[] photo = RegisterHelpers.ProcessPhotoFile(request.PhotoFile);

            var user=_mapper.Map<UserInformation>(request);

            var appUser =_mapper.Map<AppUser>(request);
            
            var randomPassword = _randomPasswordService.GenerateRandomPassword();

            user.Id = Guid.NewGuid();
            appUser.Id = Guid.NewGuid();
            user.AppUser = appUser;
            user.AppUserId = appUser.Id;
            appUser.UserId = user.Id;
            appUser.UserInformation = user;

            var registererUser = await _userManager.FindByIdAsync(registererId);
            if (await _userManager.IsInRoleAsync(registererUser, "manager"))
            {
                if (registererUser.Company == null || registererUser.CompanyId == null || registererUser.CompanyId.ToString() == "")
                    throw new Exception("Hiçbir şirkete bağlı değilsiniz. Önce bir şirkete atamanızın yapılması gerekmektedir.");

                if (registererUser.Company.FoundationYear > appUser.UserInformation.StartDate.Year)
                    throw new Exception("İşe başlama tarihi, şirket kuruluş tarihinden önce olamaz.");

                appUser.CompanyId = registererUser.CompanyId;
                appUser.Company = registererUser.Company;
            }
            var userCreateResult = await _userWriteRepository.AddAsync(user);

            await RegisterHelpers.CreateAppUser(_userManager, appUser, randomPassword);

            if (!userCreateResult)
                throw new Exception("Kullanıcı oluşturulurken bir hata oluştu.");

            await RegisterHelpers.AssignRole(_userManager, principal, appUser, registererId);

            await _userWriteRepository.SaveAsync();

            await _randomPasswordService.SendPasswordToEmailAsync(appUser.Email, randomPassword);

            return Unit.Value;
        }
    }
}
