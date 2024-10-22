using IKProject.Application.Features.Querries.GetUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.GetCompany
{
    public class GetCompaniesQuery : IRequest<List<GetCompaniesQueryResponse>>
    {
        public string Token { get; set; }
    }
}
