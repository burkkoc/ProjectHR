using IK.Domain.Entities.Abstract;
using IK.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.DTOs.RequestDTOs
{
    public class LeaveRequestDTO
    {
        public Guid Id { get; set; }
        public LeaveType LeaveType { get; set; }// yıllık izin
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public int DaysCount => (LeaveEndDate - LeaveStartDate).Days + 1;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime? Added { get; set; }

    }
}
