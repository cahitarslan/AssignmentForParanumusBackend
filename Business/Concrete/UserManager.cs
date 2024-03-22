using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete.Identity;

namespace Business.Concrete;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public async Task<List<AppRole>> GetUserRoleByIdAsync(int userId)
    {
        return await _userDal.GetUserRoleByIdAsync(userId);
    }
}
