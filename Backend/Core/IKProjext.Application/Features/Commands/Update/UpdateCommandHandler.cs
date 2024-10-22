using IK.Domain.Entities.Identity;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Application.Interfaces.Token;
using IKProject.Application.Methods.Update;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Commands.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly UpdateHelper _updateHelper;

        public UpdateCommandHandler(UserManager<AppUser> userManager, IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, UpdateHelper updateHelper)
        {
            _userManager = userManager;
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
            _updateHelper = updateHelper;
        }

        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var token = _updateHelper.GetToken();
            var principal = _updateHelper.GetPrincipalFromToken(token);
            var userId = _updateHelper.GetUserId(principal);

            var appUser = await _userManager.FindByIdAsync(userId);
            //var user2 = await _userReadRepository.GetSingleAsync(u => u.AppUserId == Guid.Parse(userId));

            appUser.UserInformation.Photo = await _updateHelper.ProcessFile(request.PhotoFile);
            appUser.PhoneNumber = request.PhoneNumber;
            appUser.UserInformation.Address = request.Address;

            _userWriteRepository.Update(appUser.UserInformation);
            var result = await _userManager.UpdateAsync(appUser);

            if (!result.Succeeded)
            {
                throw new Exception("User update failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            await _userWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
