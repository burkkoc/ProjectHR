using IKProject.Application.Methods.Request;
using IKProject.Application.Features.Querries.Requests.Advance;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediatR;
using IKProject.Application.Methods.Get;
using IK.Domain.Enums;
using IKProject.Application.Features.Querries.GetAllRequests;
using IKProject.Application.Features.Commands.Requests.Advance.Delete;
using IKProject.Application.Features.Commands.Requests.Expense.Create;
using IKProject.Application.Features.Commands.Requests.Expense.Delete;
using IKProject.Application.Features.Commands.Requests.Leave.Create;
using IKProject.Application.Features.Commands.Requests.Leave.Delete;
using IKProject.Application.Features.Commands.ApproveOrReject;
using IKProject.Application.Features.Commands.Requests.Advance.Create;

namespace IKProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GetHelper _getHelper;

        public RequestController(IMediator mediator, GetHelper getRequestHelper)
        {
            _mediator = mediator;
            _getHelper = getRequestHelper;
        }

        [HttpPost("CreateExpense")]
        public async Task<IActionResult> CreateExpense([FromForm] CreateExpenseRequestCommand command)
        {
            try
            {
                var expenseRequestId = await _mediator.Send(command);
                return Ok(new { message = "Expense request created successfully", expenseRequestId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Expense request creation failed", error = ex.Message });
            }
        }

        [HttpPost("CreateAdvance")]
        public async Task<IActionResult> CreateAdvanceRequest([FromForm] CreateAdvanceRequestCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Advance request created successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Advance request creation failed", error = ex.Message });
            }
        }

        [HttpPost("CreateLeave")]
        public async Task<IActionResult> CreateLeave([FromForm] CreateLeaveRequestCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Leave request created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Leave request creation failed", error = ex.Message });
            }
        }

        [HttpGet("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests()
        {
            try
            {
                var token = _getHelper.GetToken();
                var query = new GetAllRequestsQuery { Token = token };
                var result = await _mediator.Send(query);
                return Ok(new { message = "Requests retrieved successfully", data = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = "Unauthorized access", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to retrieve requests", error = ex.Message });
            }
        }
        [HttpDelete("DeleteAdvanceRequest/{requestId}")]
        public async Task<IActionResult> DeleteAdvanceRequest(Guid requestId)
        {
            try
            {
                var command = new DeleteAdvanceRequestCommand { RequestId = requestId };
                await _mediator.Send(command);
                return Ok(new { message = "Advance request deleted successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = "Unauthorized access", error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to delete advance request", error = ex.Message });
            }
        }
        [HttpDelete("DeleteExpenseRequest/{requestId}")]
        public async Task<IActionResult> DeleteExpenseRequest(Guid requestId)
        {
            try
            {
                var command = new DeleteExpenseRequestCommand { RequestId = requestId };
                await _mediator.Send(command);
                return Ok(new { message = "Expense request deleted successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = "Unauthorized access", error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to delete expense request", error = ex.Message });
            }
        }
        [HttpDelete("DeleteLeaveRequest/{requestId}")]
        public async Task<IActionResult> DeleteLeaveRequest(Guid requestId)
        {
            try
            {
                var command = new DeleteLeaveRequestCommand { RequestId = requestId };
                await _mediator.Send(command);
                return Ok(new { message = "Leave request deleted successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = "Unauthorized access", error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to delete leave request", error = ex.Message });
            }
        }
        [HttpPost("ApproveOrReject")]
        public async Task<IActionResult> ApproveOrReject([FromBody] ApproveOrRejectRequestCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Talep durumu başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Talep durumu güncellenemedi.", error = ex.Message });
            }
        }
    }
}
