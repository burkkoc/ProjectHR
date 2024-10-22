using IK.Domain.Entities.Concrete;
using IKProject.Application.Interfaces.Repositories.RequestRepos;
using IKProject.Persistence.Context;

namespace IKProject.Persistence.Repositories.RequestRepos
{
    public class ExpenseRequestReadRepository : ReadRepository<ExpenseRequest>, IExpenseRequestReadRepository
    {
        public ExpenseRequestReadRepository(IKProjectDbContext context) : base(context)
        {
        }
    }
}
