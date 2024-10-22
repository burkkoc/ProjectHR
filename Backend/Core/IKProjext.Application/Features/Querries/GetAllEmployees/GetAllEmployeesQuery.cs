using IKProject.Application.Features.Querries.GetAllManagers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<List<GetAllEmployeesQueryResponse>>
    {
        public string Token { get; set; }
    }
}
