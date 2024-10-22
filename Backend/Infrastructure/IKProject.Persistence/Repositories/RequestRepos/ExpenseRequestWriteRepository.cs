using IK.Domain.Entities.Concrete;
using IKProject.Application.Interfaces.Repositories.RequestRepos;
using IKProject.Persistence.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using IK.Domain.Entities;

namespace IKProject.Persistence.Repositories.RequestRepos
{
    public class ExpenseRequestWriteRepository : WriteRepository<ExpenseRequest>, IExpenseRequestWriteRepository
    {
        private readonly IKProjectDbContext _context;
        public ExpenseRequestWriteRepository(IKProjectDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddAppUserExpenseRequest(AppUserExpenseRequest appUserExpenseRequest)
        {
            try
            {
                _context.Set<AppUserExpenseRequest>().Add(appUserExpenseRequest);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> RemoveRelatedRecordsAsync(Guid expenseRequestId)
        {
            try
            {
                var appUserExpenseRequests = await _context.AppUserExpenseRequests
                    .Where(x => x.ExpenseRequestId == expenseRequestId)
                    .ToListAsync();

                _context.AppUserExpenseRequests.RemoveRange(appUserExpenseRequests);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
