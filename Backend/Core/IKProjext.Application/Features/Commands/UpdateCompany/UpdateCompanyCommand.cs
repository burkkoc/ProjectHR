using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Commands.UpdateCompany
{
    public class UpdateCompanyCommand : IRequest<Unit>
    {
        public string MersisNo { get; set; } 
        public string PhoneNumber { get; set; }
        public IFormFile? LogoFile { get; set; }
        public string Address { get; set; }
    }
    
}
