using IK.Domain.Entities.Abstract;
using IK.Domain.Entities.Identity;
using IKProject.Application.Interfaces;
using IKProject.Application.Interfaces.Services;
using IKProject.Application.Token;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IKProject.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public AuthService(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            ITokenHandler tokenHandler,
            SignInManager<AppUser> signInManager,
            IUserService userService,
            IMailService mailService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
            _mailService = mailService;
        }

        public async Task PasswordResetAsync(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                resetToken = WebUtility.UrlEncode(resetToken);

                await _mailService.SendPasswordResetMailAsync(email, user.Email, resetToken);
            }
        }

        public async Task<Tokenn> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int accessTokenLifeTime)
        {
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new AppUser
                    {
                        Id = Guid.NewGuid(),
                        Email = email,
                        UserName = email,
                        FirstName = name
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

                Tokenn token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }
            throw new Exception("Invalid external authentication.");
        }

        public async Task<Tokenn> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new Exception("Giriş yapılamadı...");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded) //Authentication başarılı!
            {
                Tokenn token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }
            throw new Exception();
        }

        public async Task<Tokenn> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Tokenn token = _tokenHandler.CreateAccessToken(15, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);
                return token;
            }
            else
                throw new Exception();
        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = WebUtility.UrlDecode(resetToken);

                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
            }
            return false;
        }

    }
}
