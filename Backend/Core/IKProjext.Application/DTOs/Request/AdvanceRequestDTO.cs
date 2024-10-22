using IK.Domain.Entities.Abstract;
using IK.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.DTOs.RequestDTOs
{
    public class AdvanceRequestDTO
    {
        public Guid Id { get; set; }
        public AdvanceType? AdvanceType { get; set; }
        public decimal AdvanceAmount { get; set; }

        public Currency AdvanceCurrency { get; set; }
        public string Description { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime? Added { get; set; }
    }
}
