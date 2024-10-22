using IK.Domain.Entities.Concrete;
using IK.Domain.Entities.Identity;
using IKProject.Application.Interfaces.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IKProject.Application.Methods.Register
{
    public static class RegisterHelpers
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

        public static byte[] ProcessPhotoFile(IFormFile photoFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                photoFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static async Task<byte[]> ProcessPhotoFilee(IFormFile photoFile)
        {
            if (photoFile == null)
                return null;

            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(photoFile.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                throw new Exception("Invalid file format. Only jpg, jpeg, and png files are allowed.");

            using (var memoryStream = new MemoryStream())
            {
                await photoFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static async Task<IdentityResult> CreateAppUser(UserManager<AppUser> userManager, AppUser appUser, string password)
        {
            var result = await userManager.CreateAsync(appUser, password);
            if (!result.Succeeded)
            {
                throw new Exception("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return result;
        }

        public static async Task AssignRole(UserManager<AppUser> userManager, ClaimsPrincipal principal, AppUser appUser, string registererId)
        {
            //if (registererId != null)
            //{
            //    var user = await userManager.FindByIdAsync(registererId);
            //    if (user.Company.FoundationYear.Year > appUser.User.StartDate.Year)
            //        throw new Exception("İşe başlama tarihi, şirket kuruluş tarihinden önce olamaz.");

            //    appUser.CompanyId = user.CompanyId;
            //}

            if (principal.IsInRole("admin"))
            {
                await userManager.AddToRoleAsync(appUser, "manager");
            }
            else if (principal.IsInRole("manager"))
            {
                await userManager.AddToRoleAsync(appUser, "employee");
            }
            else
                throw new Exception("You do not have permission to assign roles.");
        }
    }
}
