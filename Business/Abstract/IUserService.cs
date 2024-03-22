using Entities.Concrete.Identity;

namespace Business.Abstract;

public interface IUserService
{
    Task<List<AppRole>> GetUserRoleByIdAsync(int userId);
}
