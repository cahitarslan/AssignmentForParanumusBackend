using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Repositories;

public class EfUserDal : EfEntityRepositoryBase<AppUser, BaseDbContext>, IUserDal
{
    public EfUserDal(BaseDbContext context) : base(context)
    {
    }

    public async Task<List<AppRole>> GetUserRoleByIdAsync(int userId)
    {
        var roles = await Context.UserRoles
       .Where(ur => ur.UserId == userId)
       .Join(Context.Roles,
           ur => ur.RoleId,
           r => r.Id,
           (ur, r) => r)
       .ToListAsync();

        return roles;
    }
}
