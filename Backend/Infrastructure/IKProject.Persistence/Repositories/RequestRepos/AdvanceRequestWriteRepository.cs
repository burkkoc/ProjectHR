using IK.Domain.Entities;
using IK.Domain.Entities.Concrete;
using IKProject.Application.Interfaces.Repositories.RequestRepos;
using IKProject.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace IKProject.Persistence.Repositories.RequestRepos
{
    public class AdvanceRequestWriteRepository : WriteRepository<AdvanceRequest>, IAdvanceRequestWriteRepository
    {
        private readonly IKProjectDbContext _context;
        public AdvanceRequestWriteRepository(IKProjectDbContext context) : base(context)
        {
            _context = context;

        }
        public async Task<bool> AddAppUserAdvanceRequest(AppUserAdvanceRequest appUserAdvanceRequest)
        {
            try
            {
                _context.Set<AppUserAdvanceRequest>().Add(appUserAdvanceRequest);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveRelatedRecordsAsync(Guid advanceRequestId)
        {
            try
            {
                var appUserAdvanceRequests = await _context.AppUserAdvanceRequests

                    .Where(x => x.AdvanceRequestId == advanceRequestId)
                    .ToListAsync();

                _context.AppUserAdvanceRequests.RemoveRange(appUserAdvanceRequests);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
