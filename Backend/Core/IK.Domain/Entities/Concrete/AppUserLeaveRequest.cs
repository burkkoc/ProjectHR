using IK.Domain.Entities.Concrete;
using IK.Domain.Entities.Identity;
using System;

namespace IK.Domain.Entities
{
    public class AppUserLeaveRequest
    {
        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public Guid LeaveRequestId { get; set; }
        public virtual LeaveRequest LeaveRequest { get; set; }
    }
}
