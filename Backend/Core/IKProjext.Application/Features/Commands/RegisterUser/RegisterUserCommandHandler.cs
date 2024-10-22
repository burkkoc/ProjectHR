using AutoMapper;
using IK.Domain.Entities.Concrete;
using IK.Domain.Entities.Identity;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using IKProject.Application.Methods.Register;

namespace IKProject.Application.Features.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(UserManager<AppUser> userManager, IUserWriteRepository userWriteRepository, IMapper mapper)
        {
            _userManager = userManager;
            _userWriteRepository = userWriteRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            byte[] photo = await RegisterHelpers.ProcessPhotoFilee(request.PhotoFile);

            var user = _mapper.Map<UserInformation>(request);
            user.Photo = photo;

            var appUser = _mapper.Map<AppUser>(request);

            await _userWriteRepository.AddAsync(user);

            await RegisterHelpers.CreateAppUser(_userManager, appUser, request.Password);
            await _userManager.AddToRoleAsync(appUser, "admin");

            await _userWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
