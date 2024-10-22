using IK.Domain.Entities;
using IK.Domain.Entities.Concrete;

namespace IKProject.Application.Interfaces.Repositories.RequestRepos
{
    public interface ILeaveRequestWriteRepository : IWriteRepository<LeaveRequest>
    {
        Task<bool> AddAppUserLeaveRequest(AppUserLeaveRequest appUserLeaveeRequest);

    }
}
