using Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Entities.Concrete.Identity;

public class AppUser : IdentityUser<int>, IEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
