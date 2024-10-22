using IK.Domain.Entities.Abstract;
using IK.Domain.Entities.Identity;
using IKProject.Application.Interfaces;
using IKProject.Persistence.Context;

public class UserService : IUserService
{
    private readonly IKProjectDbContext _dbContext;

    public UserService(IKProjectDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IUser GetUserById(Guid Id)
    {
        return _dbContext.UserInformations.FirstOrDefault(x => x.Id == Id);
    }

    public ICollection<IUser> GetAll()
    {
        return _dbContext.UserInformations.ToList<IUser>();
    }

    public Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime expiration, int addOnMinutes)
    {
        throw new NotImplementedException();
    }
   
}
