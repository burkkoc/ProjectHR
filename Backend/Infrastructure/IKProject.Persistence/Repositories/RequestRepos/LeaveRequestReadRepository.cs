using IK.Domain.Entities.Concrete;
using IKProject.Application.Interfaces.Repositories.RequestRepos;
using IKProject.Persistence.Context;

namespace IKProject.Persistence.Repositories.RequestRepos
{
    public class LeaveRequestReadRepository : ReadRepository<LeaveRequest>, ILeaveRequestReadRepository
    {
        public LeaveRequestReadRepository(IKProjectDbContext context) : base(context)
        {
        }
    }
}
