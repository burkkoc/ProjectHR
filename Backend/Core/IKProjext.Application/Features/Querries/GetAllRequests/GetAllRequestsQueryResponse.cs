using IK.Domain.Entities;
using IK.Domain.Entities.Abstract;
using IK.Domain.Entities.Concrete;
using IK.Domain.Enums;
using IKProject.Application.DTOs.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.GetAllRequests
{
    public class GetAllRequestsQueryResponse
    {

        public List<AdvanceRequestDTO> AdvanceRequests { get; set; }
        public List<ExpenseRequestDTO> ExpenseRequests { get; set; }
        public List<LeaveRequestDTO> LeaveRequests { get; set; }
        
    }
}
