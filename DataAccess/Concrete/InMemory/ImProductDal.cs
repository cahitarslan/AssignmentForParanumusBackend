using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory;

public class ImProductDal : IProductDal
{
    private List<Product> _products;

    public ImProductDal()
    {
        _products = new()
        {
            new() { Id = 1, Name = "Phone", Price = 10000, Description = "Description for Phone" },
            new() { Id = 2, Name = "Desktop", Price = 30000, Description = "Description for Desktop" },
            new() { Id = 3, Name = "Laptop", Price = 40000, Description = "Description for Laptop" },
            new() { Id = 4, Name = "TV", Price = 50000, Description = "Description for TV" },
        };
    }

    public async Task AddAsync(Product product)
    {
        product.Id = _products.Count + 1;
        _products.Add(product);
    }

    public async Task DeleteAsync(Product product)
    {
        var deletedProduct = _products.SingleOrDefault(p => p.Id == product.Id);
        if (deletedProduct != null)
        {
            _products.Remove(deletedProduct);
        }
        else
        {
            throw new Exception("No such product found");
        }
    }

    public async Task<Product> GetAsync(Expression<Func<Product, bool>> filter)
    {
        return _products.AsQueryable().Where(filter).SingleOrDefault();
    }

    public async Task<List<Product>> GetAllAsync(Expression<Func<Product, bool>> filter = null)
    {
        return filter == null ? _products.ToList() : _products.AsQueryable().Where(filter).ToList();
    }

    public async Task UpdateAsync(Product product)
    {
        var updatedProduct = _products.SingleOrDefault(p => p.Id == product.Id);
        if (updatedProduct != null)
        {
            updatedProduct.Name = product.Name;
            updatedProduct.Price = product.Price;   
            updatedProduct.Description = product.Description;
        }
        else
        {
            throw new Exception("No such product found");
        }
    }
}
