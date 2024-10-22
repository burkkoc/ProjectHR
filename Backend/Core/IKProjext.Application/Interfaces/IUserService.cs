using IK.Domain.Entities.Abstract;
using IK.Domain.Entities.Concrete;
using IK.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Interfaces
{
    public interface IUserService
    {
        IUser GetUserById(Guid Id);

        ICollection<IUser> GetAll();
        //Task<Guid> RegisterUserAsync(User user);
        //IUser GetByEmailAsync(string Email);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime expiration, int addOnMinutes);
    }
}
