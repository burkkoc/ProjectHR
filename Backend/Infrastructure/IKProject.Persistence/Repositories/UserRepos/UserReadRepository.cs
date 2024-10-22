using IK.Domain.Entities.Concrete;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Persistence.Repositories.UserRepos
{
    public class UserReadRepository : ReadRepository<UserInformation>, IUserReadRepository
    {
        public UserReadRepository(IKProjectDbContext context) : base(context)
        {
        }
        //public async Task<UserInformation> GetByIdAsync(string id)
        //{
        //    return await Table.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        //}

    }
}
