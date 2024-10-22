using IK.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IK.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }

        public string? VerificationCode { get; set; }
        public DateTime? VerificationCodeExpiration{ get; set; }

        [ForeignKey("User")]
        public Guid? UserId { get; set; }
        public virtual UserInformation? UserInformation { get; set; }

        [ForeignKey("CompanyId")]
        public Guid? CompanyId { get; set; }
        public virtual Company? Company { get; set; }
        public bool MustChangePassword { get; set; } = false;

        public virtual ICollection<AppUserLeaveRequest> AppUserLeaveRequests { get; set; }// = new List<AppUserLeaveRequest>();
        public virtual ICollection<AppUserExpenseRequest> AppUserExpenseRequests { get; set; }// = new List<AppUserExpenseRequest>();
        public virtual ICollection<AppUserAdvanceRequest> AppUserAdvanceRequests { get; set; }// = new List<AppUserAdvanceRequest>();


    }
}
