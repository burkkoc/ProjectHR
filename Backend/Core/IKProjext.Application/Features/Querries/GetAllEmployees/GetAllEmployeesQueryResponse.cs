using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.GetAllEmployees
{
    public class GetAllEmployeesQueryResponse
    {
        public string TC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? SecondName { get; set; }
        public string? SecondLastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Profession { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; }
        public int? Salary { get; set; }
    }
}
