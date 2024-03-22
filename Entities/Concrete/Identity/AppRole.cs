using Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Entities.Concrete.Identity;

public class AppRole : IdentityRole<int>, IEntity
{
}
