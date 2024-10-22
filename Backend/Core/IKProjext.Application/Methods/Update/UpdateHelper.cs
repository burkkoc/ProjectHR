using IKProject.Application.Interfaces.Token;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IKProject.Application.Methods.Update
{
    public class UpdateHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenServices _tokenServices;

        public UpdateHelper(IHttpContextAccessor httpContextAccessor, ITokenServices tokenServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenServices = tokenServices;
        }

        public string GetToken()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                throw new Exception("Invalid or missing Authorization header");
            }

            return authorizationHeader.Substring("Bearer ".Length).Trim();
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var principal = _tokenServices.GetClaimsPrincipalFromExpiredToken(token);
            if (principal == null)
            {
                throw new Exception("Geçersiz token");
            }
            return principal;
        }

        public async Task<byte[]> ProcessFile(IFormFile file)
        {
            if (file == null) return null;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public string GetUserId(ClaimsPrincipal principal)
        {
            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new Exception("Kullanıcı kimliği bulunamadı");
            }
            return userId;
        }

        public void EnsureUserIsAdmin(ClaimsPrincipal principal)
        {
            if (!principal.IsInRole("admin"))
            {
                throw new Exception("Şirket bilgilerini güncellemeye yetkiniz yok.");
            }
        }
    }
}
