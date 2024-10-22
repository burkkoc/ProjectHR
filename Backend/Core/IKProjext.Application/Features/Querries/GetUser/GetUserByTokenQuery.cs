using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.GetUser
{
    public class GetUserByTokenQuery:IRequest<GetUserByTokenResponse>
    {
        public string Token { get; set; }
    }
}
