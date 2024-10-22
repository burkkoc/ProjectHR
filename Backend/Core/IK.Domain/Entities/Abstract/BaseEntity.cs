using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Domain.Entities.Abstract
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual DateTime Added { get; set; }
        public virtual DateTime? Updated { get; set; }
        public virtual DateTime? Deleted { get; set; }
    }
}
