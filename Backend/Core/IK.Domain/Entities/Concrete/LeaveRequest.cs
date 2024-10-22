using IK.Domain.Entities.Abstract;
using IK.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Domain.Entities.Concrete
{
    public class LeaveRequest:Request
    {
        public LeaveType LeaveType { get; set; }// yıllık izin
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public int DaysCount => (LeaveEndDate - LeaveStartDate).Days + 1;

        public virtual ICollection<AppUserLeaveRequest> AppUserLeaveRequests { get; set; }// = new List<AppUserLeaveRequest>();
    }
}
