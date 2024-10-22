using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Commands.SetManagerToCompany
{
    public class SetManagerCommand : IRequest<Unit>
    {
        public string? UserEmail { get; set; }
        public string? CompanyEmail { get; set; }


    }
}
