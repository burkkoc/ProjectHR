using IK.Domain.Entities.Concrete;
using IKProject.Application.Features.Commands.Register;
using IKProject.Application.Features.Commands.RegisterCompany;
using IKProject.Application.Features.Commands.SetManagerToCompany;
using IKProject.Application.Features.Commands.UpdateCompany;
using IKProject.Application.Features.Querries.GetCompany;
using IKProject.Application.Features.Querries.GetSingleCompany;
using IKProject.Application.Features.Querries.GetUser;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Application.Methods.Get;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GetHelper _getHelper;

        public CompanyController(IMediator mediator, GetHelper getHelper)
        {
            _mediator = mediator;
            _getHelper = getHelper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterCompanyCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("Company registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Company registration failed", error = ex.Message });
            }
        }

        [HttpGet("GetAllCompanies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var result = await _getHelper.HandleGetAllCompaniesQuery();
                return Ok(new { message = "Companies retrieved successfully", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve companies", error = ex.Message });
            }
        }


        [HttpPost("SetManager")]
        public async Task<IActionResult> SetManager([FromForm] SetManagerCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("SET MANAGER -- SUCCESS");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "SET MANAGER -- FAILED", error = ex.Message });
            }

        }
        [HttpGet("GetCompany")]
        public async Task<IActionResult> GetCompany()
        {
            try
            {
                var result = await _getHelper.HandleGetSingleCompanyQuery();
                return Ok(new { message = "Company retrieved successfully", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve company", error = ex.Message });
            }
        }
        [HttpPatch("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany([FromForm] UpdateCompanyCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("Company updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Company update failed", error = ex.Message });
            }
        }
    }
}
