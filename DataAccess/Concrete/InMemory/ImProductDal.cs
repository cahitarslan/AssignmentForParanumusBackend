using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory;

public class ImProductDal : IProductDal
{
    public List<Product> _products;

    public ImProductDal()
    {
        _products = new()
        {
            #region Products

            new() { Id = 1, Name = "Book 1", Price = 10, Description = "Description for Book 1" },
            new() { Id = 2, Name = "Book 2", Price = 20, Description = "Description for Book 2" },
            new() { Id = 3, Name = "Book 3", Price = 15, Description = "Description for Book 3" },
            new() { Id = 4, Name = "Book 4", Price = 30, Description = "Description for Book 4" }

	        #endregion
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
