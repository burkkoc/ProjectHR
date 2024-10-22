using IK.Domain.Entities.Concrete;
using IKProject.Application.Interfaces.Repositories.RequestRepos;
using IKProject.Persistence.Context;

namespace IKProject.Persistence.Repositories.RequestRepos
{
    public class AdvanceRequestReadRepository : ReadRepository<AdvanceRequest>, IAdvanceRequestReadRepository
    {
        public AdvanceRequestReadRepository(IKProjectDbContext context) : base(context)
        {
        }
    }
}
