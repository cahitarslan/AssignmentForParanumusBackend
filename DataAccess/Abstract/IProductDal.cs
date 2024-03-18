using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IProductDal : IEntityRepositoryAsync<Product>
{
}
