using FluentValidation;
using FluentValidation.Results;
using IK.Domain.Entities.Concrete;
using IKProject.Application.Features.Commands.Register;
using IKProject.Application.Features.Commands.RegisterUser;
using IKProject.Application.Features.Commands.Update;
using IKProject.Application.Features.Querries.GetAllManagers;
using IKProject.Application.Features.Querries.GetUser;
using IKProject.Application.Features.Querries.Login;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Application.Methods.Get;
using IKProject.Application.Validators;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IKProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GetHelper _getHelper;

        public UserController(IMediator mediator, GetHelper getHelper)
        {
            _mediator = mediator;
            _getHelper = getHelper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "User registration failed", error = ex.Message });
            }
        }
        [HttpPost("RegisterEmployee")]
        public async Task<IActionResult> RegisterEmployee([FromForm] RegisterCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok("Employee registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Employee registration failed", error = ex.Message });
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginQueryRequest query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(new{
                    result.Token,
                    result.MustChangePassword
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Login failed", error = ex.Message });
            }
        }
        [HttpGet("GetUserByToken")]
        public async Task<IActionResult> GetUserByToken()
        {
            try
            {
                var result = await _getHelper.HandleGetUserByTokenQuery();
                return Ok(new { message = "User retrieved successfully", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve user", error = ex.Message });
            }
        }
        [HttpPatch("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "User update failed", error = ex.Message });
            }
        }
       
        [HttpGet("GetAllManagers")]
        public async Task<IActionResult> GetAllManagers()
        {
            try
            {
                var result = await _getHelper.HandleGetAllManagersQuery();
                return Ok(new { message = "Managers retrieved successfully", data = result});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve user", error = ex.Message });
            }
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var result = await _getHelper.HandleGetAllEmployeesQuery();
                return Ok(new { message = "Employees retrieved successfully", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to retrieve user", error = ex.Message });
            }
        }
        [HttpPost("kullaniciOlustur")]
        public async Task<IActionResult> KullaniciOlustur([FromForm] RegisterUserCommand command)
        {
            if (command.PhotoFile == null || command.PhotoFile.Length == 0)
            {
                return BadRequest("PhotoFile field is required.");
            }

            try
            {
                await _mediator.Send(command);
                return Ok("Kullanıcı başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Kullanıcı oluşturma başarısız", error = ex.Message });
            }
        }
    }
}
