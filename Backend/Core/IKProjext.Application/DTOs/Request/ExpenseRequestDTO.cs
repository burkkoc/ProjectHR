using IK.Domain.Entities;
using IK.Domain.Entities.Abstract;
using IK.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.DTOs.RequestDTOs
{
    public class ExpenseRequestDTO
    {
        public Guid Id { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public decimal ExpenseAmount { get; set; }

        public Currency ExpenseCurrency { get; set; }

        public byte[] Document { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public RequestStatus RequestStatus { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime? Added { get; set; }

    }
}
