using Business.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract;

public interface IProductService
{
    Task<IDataResult<List<Product>>> GetAllAsync();
    Task<IDataResult<Product>> GetByIdAsync(int id);
    Task<IResult> AddAsync(Product product);
    Task<IResult> UpdateAsync(Product product);
    Task<IResult> DeleteAsync(Product product);
}
