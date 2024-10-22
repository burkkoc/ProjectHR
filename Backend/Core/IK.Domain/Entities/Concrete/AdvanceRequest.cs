using IK.Domain.Entities.Abstract;
using IK.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Domain.Entities.Concrete
{
    public class AdvanceRequest:Request
    {
        public AdvanceType? AdvanceType { get; set; } 
        public decimal AdvanceAmount { get; set; }

        public Currency AdvanceCurrency {  get; set; }
        public string Description { get; set; }


        public virtual ICollection<AppUserAdvanceRequest> AppUserAdvanceRequests { get; set; }
    }
}
