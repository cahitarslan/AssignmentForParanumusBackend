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
        // var userRoleIds = await Context.UserRoles
        //    .Where(ur => ur.UserId == userId)
        //    .Select(ur => ur.RoleId)
        //    .ToListAsync();

        //var roles = await Context.Roles
        //    .Where(r => userRoleIds.Contains(r.Id))
        //    .ToListAsync();

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
