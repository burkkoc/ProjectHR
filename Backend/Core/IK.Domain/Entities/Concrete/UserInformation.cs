using IK.Domain.Entities.Abstract;
using IK.Domain.Entities.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IK.Domain.Entities.Concrete
{
    public class UserInformation : BaseEntity, IUser
    {
        public string TC { get; set; }
        public string FirstName { get; set; }
        public byte[] Photo { get; set; }
        public string LastName { get; set; }
        public override Guid Id { get; set; }
        public override bool IsActive { get; set; }
        public override DateTime Added { get; set; }
        public override DateTime? Updated { get; set; }
        public override DateTime? Deleted { get; set; }
        public string? SecondName { get; set; }
        public string? SecondLastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Profession { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }

        [ForeignKey("AppUser")]
        public Guid? AppUserId { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public int? Salary { get; set; }

       
    }
}
