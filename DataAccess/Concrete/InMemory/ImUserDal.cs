using DataAccess.Abstract;
using Entities.Concrete.Identity;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory;

public class ImUserDal : ImEntityRepositoryBase<AppUser>, IUserDal
{
    public List<AppUser> _users;
    public List<AppRole> _roles;
    public List<AppUserRole> _userRoles;

    public ImUserDal()
    {
        _users = new()
        {
            #region Users

            new() { Id = 1, FirstName = "Cahit", LastName = "Arslan", UserName = "cahit@xyz.com", NormalizedUserName = "CAHIT@XYZ.COM", Email = "cahit@xyz.com", NormalizedEmail = "CAHIT@XYZ.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEIVtkjUir56hvfjooppnSKWa4rz5sFyQXa8JFZ1gK2tquXGT6Mrpm0xV4cGMnGOuqg==", SecurityStamp = "BGL56R3AGG36QC3L57PCNBCKR67KUNDJ", ConcurrencyStamp = "8589cfe2-1fd1-4081-8497-6354da859193", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0  },
            new() { Id = 2, FirstName = "Ahmet", LastName = "Yılmaz", UserName = "ahmet@xyz.com", NormalizedUserName = "AHMET@XYZ.COM", Email = "ahmet@xyz.com", NormalizedEmail = "AHMET@XYZ.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEJ0mB4+wZ3qjHPz9vp5g/i1B8tkrS8Bu5T3FxQliPBoZ2tqaLj+cmDUdN6LMiEzubw==", SecurityStamp = "US75LHFSJCOKMLCGFKDNOIONICCEUOSX", ConcurrencyStamp = "53242214-d1ef-41f5-90f2-78b8a085bd76", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0  },
             new() { Id = 3, FirstName = "Ayşe", LastName = "Öztürk", UserName = "ayse@xyz.com", NormalizedUserName = "AYSE@XYZ.COM", Email = "ayse@xyz.com", NormalizedEmail = "AYSE@XYZ.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEL7K3WoDfVir09s0qhsY/MWEGwtkGUqCvDPFSVR++cnpsO3yTU8JqdXFBhBf1AJbIw==", SecurityStamp = "MYBC2DBVXHIKOTZI6O4BNFMCNNETLGQ2", ConcurrencyStamp = "4e13caf9-bbc6-4a85-b9fb-2aa25f9a20fe", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0  },
            #endregion
        };

        _roles = new()
        {
            #region Roles

            new() { Id = 1, Name = "regular" },
            new() { Id = 2, Name = "employee" }

	        #endregion
        };

        _userRoles = new()
        {
            #region UserRoles

            new(){ UserId = 1, RoleId = 1 },
            new(){ UserId = 2, RoleId = 1 },
            new(){ UserId = 3, RoleId = 2 },

	        #endregion
        };
    }

    public async override Task AddAsync(AppUser user)
    {
        user.Id = _users.Count + 1;
        _users.Add(user);
    }

    public async override Task DeleteAsync(AppUser user)
    {
        var deletedUser = _users.SingleOrDefault(u => u.Id == user.Id);
        if (deletedUser != null)
        {
            _users.Remove(deletedUser);
        }
        else
        {
            throw new Exception("No such user found");
        }
    }

    public async override Task UpdateAsync(AppUser user)
    {
        var updatedUser = _users.SingleOrDefault(u => u.Id == user.Id);
        if (updatedUser != null)
        {
            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            updatedUser.UserName = user.UserName;
            updatedUser.Email = user.Email;
            //...
        }
        else
        {
            throw new Exception("No such user found");
        }
    }

    public async override Task<AppUser> GetAsync(Expression<Func<AppUser, bool>> filter)
    {
        //return base.GetAsync(filter);
        return _users.AsQueryable().Where(filter).SingleOrDefault();
    }

    public async override Task<List<AppUser>> GetAllAsync(Expression<Func<AppUser, bool>> filter = null)
    {
        //return base.GetAllAsync(filter);
        return filter == null ? _users.ToList() : _users.AsQueryable().Where(filter).ToList();
    }

    public async Task<List<AppRole>> GetUserRoleByIdAsync(int userId)
    {
        var userRoles = _userRoles.Where(ur => ur.UserId == userId).Select(ur => ur.RoleId);
        var roles = _roles.Where(role => userRoles.Contains(role.Id)).ToList();
        return roles;
    }


    public class AppUserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}

