using IK.Domain.Entities;
using IK.Domain.Entities.Concrete;
using IKProject.Application.Interfaces.Repositories.RequestRepos;
using IKProject.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace IKProject.Persistence.Repositories.RequestRepos
{
    public class LeaveRequestWriteRepository : WriteRepository<LeaveRequest>, ILeaveRequestWriteRepository
    {
        private readonly IKProjectDbContext _context;
        public LeaveRequestWriteRepository(IKProjectDbContext context) : base(context)
        {
            _context = context;

        }
        public async Task<bool> AddAppUserLeaveRequest(AppUserLeaveRequest appUserLeaveRequest)
        {
            try
            {
                _context.Set<AppUserLeaveRequest>().Add(appUserLeaveRequest);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> RemoveRelatedRecordsAsync(Guid leaveRequestId)
        {
            try
            {
                var appUserLeaveRequests = await _context.AppUserLeaveRequests
                    .Where(x => x.LeaveRequestId == leaveRequestId)
                    .ToListAsync();

                _context.AppUserLeaveRequests.RemoveRange(appUserLeaveRequests);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
