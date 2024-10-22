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
    public class UserWriteRepository : WriteRepository<UserInformation>, IUserWriteRepository
    {
        public UserWriteRepository(IKProjectDbContext context) : base(context)
        {
        }
    }
}
