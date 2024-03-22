using Entities.Concrete.Identity;

namespace DataAccess.Abstract;

public interface IUserDal : IEntityRepositoryAsync<AppUser>
{
    Task<List<AppRole>> GetUserRoleByIdAsync(int userId);
}
