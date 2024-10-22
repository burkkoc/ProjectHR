using IK.Domain.Entities;
using IK.Domain.Entities.Concrete;

namespace IKProject.Application.Interfaces.Repositories.RequestRepos
{
    public interface IAdvanceRequestWriteRepository : IWriteRepository<AdvanceRequest>
    {
        Task<bool> AddAppUserAdvanceRequest(AppUserAdvanceRequest appUserAdvanceRequest);
        
    }
}
