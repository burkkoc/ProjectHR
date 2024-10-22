using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Features.Querries.Login
{
    public class LoginQueryResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool MustChangePassword { get; set; }
    }
}
