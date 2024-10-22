using IK.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Security.Claims;
using IKProject.Application.Interfaces.Token;

namespace IKProject.Application.Methods.Request
{
    public static class RequestHelpers
    {
        public static ClaimsPrincipal ValidateToken(ITokenServices tokenServices, IHttpContextAccessor httpContextAccessor)
        {
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
                throw new Exception("Invalid or missing Authorization header");

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var principal = tokenServices.GetClaimsPrincipalFromExpiredToken(token);
            if (principal == null)
                throw new Exception("Invalid token.");

            return principal;
        }

        public static string GetUserId(ClaimsPrincipal principal)
        {
            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                throw new Exception("User ID not found in token.");
            return userId;
        }

        public static byte[] ProcessDocumentFile(IFormFile documentFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                documentFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static void ValidateDescription(AdvanceType? advanceType, string description)
        {
            if (advanceType == AdvanceType.individual && string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("The Description field is required when AdvanceType is individual.");
            }
        }

        public static string GetTokenFromHeader(IHttpContextAccessor httpContextAccessor)
        {
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                throw new Exception("Invalid or missing Authorization header.");
            }
            return authorizationHeader.Substring("Bearer ".Length).Trim();
        }

        public static ClaimsPrincipal GetPrincipalFromToken(string token, ITokenServices tokenServices)
        {
            var principal = tokenServices.GetClaimsPrincipalFromExpiredToken(token);
            if (principal == null)
            {
                throw new Exception("Invalid token.");
            }
            return principal;
        }
    }
}
