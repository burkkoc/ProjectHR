using IK.Domain.Entities.Concrete;
using IK.Domain.Entities.Identity;
using IK.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Domain.Entities.Abstract
{
    public abstract class Request : BaseEntity
    {

        [ForeignKey("AppUser")]
        public Guid UserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public RequestStatus RequestStatus { get; set; }
        //public DateTime RequestDate { get; set; }
        public DateTime? ResponseDate { get; set; }
    }
}
