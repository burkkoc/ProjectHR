using IK.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Commands.RegisterCompany
{
    public class RegisterCompanyCommand : IRequest<Unit>
    {
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? MersisNo { get; set; }
        public string? TaxNo { get; set; }
        public string? TaxOffice { get; set; }
        public int NumberOfEmployees { get; set; }
        public IFormFile? Logo { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public int FoundationYear { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
    }
}
