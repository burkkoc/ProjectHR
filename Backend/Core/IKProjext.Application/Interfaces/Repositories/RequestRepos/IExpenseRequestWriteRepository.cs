using IK.Domain.Entities;
using IK.Domain.Entities.Concrete;

namespace IKProject.Application.Interfaces.Repositories.RequestRepos
{
    public interface IExpenseRequestWriteRepository : IWriteRepository<ExpenseRequest>
    {
        Task<bool> AddAppUserExpenseRequest(AppUserExpenseRequest appUserExpenseRequest);
    }
}
