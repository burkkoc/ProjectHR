using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Domain.Entities.Abstract
{
    public interface IUser
    {
        public string TC { get; set; }
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public string Profession { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
    }
}
