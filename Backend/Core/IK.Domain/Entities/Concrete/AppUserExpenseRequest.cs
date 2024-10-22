using IK.Domain.Entities.Concrete;
using IK.Domain.Entities.Identity;
using System;

namespace IK.Domain.Entities
{
    public class AppUserExpenseRequest
    {
        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public Guid ExpenseRequestId { get; set; }
        public virtual ExpenseRequest ExpenseRequest { get; set; }
    }
}
