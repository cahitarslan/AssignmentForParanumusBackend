using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories;

public class EfProductDal : EfEntityRepositoryBase<Product, BaseDbContext>, IProductDal
{
    public EfProductDal(BaseDbContext context) : base(context)
    {
    }
}
