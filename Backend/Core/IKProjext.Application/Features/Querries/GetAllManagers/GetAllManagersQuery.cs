using IKProject.Application.Features.Querries.GetCompany;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.GetAllManagers
{
    public class GetAllManagersQuery : IRequest<List<GetAllManagersQueryResponse>>
    {
        public string Token { get; set; }


    }
}
