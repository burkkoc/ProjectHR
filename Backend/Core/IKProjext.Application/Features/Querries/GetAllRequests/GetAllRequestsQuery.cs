using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.GetAllRequests
{
    public class GetAllRequestsQuery : IRequest<GetAllRequestsQueryResponse>
    {
        public string Token { get; set; }
    }
}
