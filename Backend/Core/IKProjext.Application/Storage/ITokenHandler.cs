using IK.Domain.Entities.Identity;
using IKProject.Application.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Domain.Entities.Abstract
{
    public interface ITokenHandler
    {
        Tokenn CreateAccessToken(int second, AppUser appUser);
        string CreateRefreshToken();
    }
}
