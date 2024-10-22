using IKProject.Application.Features.Querries.GetCompany;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.GetSingleCompany
{
    public class GetSingleCompanyQuery : IRequest<GetSingleCompanyQueryResponse>
    {
        public string Token { get; set; }

    }
}
