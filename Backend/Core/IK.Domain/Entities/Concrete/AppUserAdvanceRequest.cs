using IK.Domain.Entities.Concrete;
using IK.Domain.Entities.Identity;
using System;

namespace IK.Domain.Entities
{
    public class AppUserAdvanceRequest
    {
        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public Guid AdvanceRequestId { get; set; }
        public virtual AdvanceRequest AdvanceRequest { get; set; }
    }
}
