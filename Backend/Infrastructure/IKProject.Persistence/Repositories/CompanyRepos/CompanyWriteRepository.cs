using IK.Domain.Entities.Concrete;
using IKProject.Application.Interfaces.Repositories;
using IKProject.Application.Interfaces.Repositories.UserRepos;
using IKProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Persistence.Repositories.CompanyRepos
{
    public class CompanyWriteRepository : WriteRepository<Company>, ICompanyWriteRepository
    {
        public CompanyWriteRepository(IKProjectDbContext context) : base(context)
        {
        }

    }
}
