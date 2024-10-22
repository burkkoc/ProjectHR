using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using MediatR;
using IKProject.Application.Features.Querries.GetAllManagers;
using IKProject.Application.Features.Querries.GetUser;
using IKProject.Application.Features.Querries.GetCompany;
using IKProject.Application.Features.Querries.GetSingleCompany;
using IKProject.Application.Interfaces.Token;
using IKProject.Application.Features.Querries.GetAllEmployees;

namespace IKProject.Application.Methods.Get
{
    public class GetHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenServices _tokenServices;
        private readonly IMediator _mediator;

        public GetHelper(IHttpContextAccessor httpContextAccessor, ITokenServices tokenServices, IMediator mediator)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenServices = tokenServices;
            _mediator = mediator;
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
                throw new Exception("Yetkiniz yok.");
            }
        }

        public void EnsureUserIsManager(ClaimsPrincipal principal)
        {
            if (!principal.IsInRole("manager"))
            {
                throw new Exception("Yetkiniz yok.");
            }
        }

        public async Task<List<GetAllManagersQueryResponse>> HandleGetAllManagersQuery()
        {
            var token = GetToken();
            var query = new GetAllManagersQuery { Token = token };
            var result = await _mediator.Send(query);
            return result;
        }

        public async Task<List<GetAllEmployeesQueryResponse>> HandleGetAllEmployeesQuery()
        {
            var token = GetToken();
            var query = new GetAllEmployeesQuery { Token = token };
            var result = await _mediator.Send(query);
            return result;
        }

        public async Task<GetUserByTokenResponse> HandleGetUserByTokenQuery()
        {
            var token = GetToken();
            var query = new GetUserByTokenQuery { Token = token };
            var result = await _mediator.Send(query);
            return result;
        }
        public async Task<GetSingleCompanyQueryResponse> HandleGetSingleCompanyQuery()
        {
            var token = GetToken();
            var query = new GetSingleCompanyQuery { Token = token };
            var result = await _mediator.Send(query);
            return result;
        }

        public async Task<List<GetCompaniesQueryResponse>> HandleGetAllCompaniesQuery()
        {
            var token = GetToken();
            var query = new GetCompaniesQuery { Token = token };
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
