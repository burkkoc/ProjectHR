using AutoMapper;
using IK.Domain.Entities.Identity;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Application.Interfaces.Token;
using IKProject.Application.Methods.Get;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.GetUser
{
    public class GetUserByTokenQueryHandler : IRequestHandler<GetUserByTokenQuery, GetUserByTokenResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserReadRepository _userReadRepository;
        private readonly GetHelper _getHelper;
        private readonly IMapper _mapper;
        public GetUserByTokenQueryHandler(UserManager<AppUser> userManager, IUserReadRepository readRepository, GetHelper getHelper,IMapper mapper)
        {
            _userManager = userManager;
            _userReadRepository = readRepository;
            _getHelper = getHelper;
            _mapper = mapper;
        }

        public async Task<GetUserByTokenResponse> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
        {

            var principal = _getHelper.GetPrincipalFromToken(request.Token);
            var userId = _getHelper.GetUserId(principal);

            var appUser = await _userManager.FindByIdAsync(userId);
            //var user = await _userReadRepository.GetByIdAsync(appUser.UserId.ToString());

            if (appUser == null)
                throw new Exception("Kullanıcı bulunamadı");

            //var y = _mapper.Map<GetUserByTokenResponse>(user);

            return _mapper.Map<GetUserByTokenResponse>(appUser);
        }
    }
}