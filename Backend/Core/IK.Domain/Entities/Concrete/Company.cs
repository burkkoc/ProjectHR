using IK.Domain.Entities.Abstract;
using IK.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Domain.Entities.Concrete
{
    public class Company : BaseEntity
    {
        public override Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string MersisNo { get; set; }
        public string TaxNo { get; set; }
        public string TaxOffice { get; set; }
        public int NumberOfEmployees { get; set; }

        public byte[] Logo { get; set; }

        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int FoundationYear { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public override bool IsActive { get; set; }
        public override DateTime Added { get; set; }
        public override DateTime? Updated { get; set; }
        public override DateTime? Deleted { get; set; }
        public virtual ICollection<AppUser>? AppUser { get; set; }





    }
}
