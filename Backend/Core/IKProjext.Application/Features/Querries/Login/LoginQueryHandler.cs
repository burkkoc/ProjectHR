using AutoMapper;
using IK.Domain.Entities.Identity;
using IKProject.Application.Features.Querries.Login;
using IKProject.Application.Interfaces.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries
{
    public class LoginQueryHandler : IRequestHandler<LoginQueryRequest, LoginQueryResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        public LoginQueryHandler(UserManager<AppUser> userManager, ITokenServices tokenServices,IMapper mapper)
        {
            _userManager = userManager;
            _tokenServices = tokenServices;
            _mapper = mapper;

        }
        public async Task<LoginQueryResponse> Handle(LoginQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı - kullanıcı bulunamadı");
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            
            if (!result)
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı - şifre doğrulaması başarısız");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = await _tokenServices.CreateToken(user, roles);

            var response = _mapper.Map<LoginQueryResponse>(user);
            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            response.Expiration = token.ValidTo;
            //response.MustChangePassword = user.MustChangePassword;
            return response;
        }
    }
}