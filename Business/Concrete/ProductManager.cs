using Business.Abstract;
using Business.Services;
using Business.Utilities.Consts;
using Business.Utilities.Results;
using Business.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Business.Concrete;

public class ProductManager : IProductService
{
    private readonly IProductDal _productDal;
    private readonly IValidator<Product> _validator;
    private readonly ICacheService _cacheService;

    public ProductManager(IProductDal productDal, IValidator<Product> validator, ICacheService cacheService)
    {
        _productDal = productDal;
        _validator = validator;
        _cacheService = cacheService;
    }


    public async Task<IResult> AddAsync(Product product)
    {
        var validationResult = _validator.Validate(product);
        if (!validationResult.IsValid)
        {
            var messages = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            return new ErrorResult(messages);
        }
        await _productDal.AddAsync(product);
        _cacheService.RemoveData(CacheKeys.Product.AllProducts);
        return new SuccessResult(ResultMessages.Success.ProductAdd);
    }

    public async Task<IResult> DeleteAsync(Product product)
    {
        await _productDal.DeleteAsync(product);
        _cacheService.RemoveData(CacheKeys.Product.AllProducts);
        return new SuccessResult(ResultMessages.Success.ProductDelete);
    }

    public async Task<IDataResult<List<Product>>> GetAllAsync()
    {
        var cachedProducts = _cacheService.GetData<List<Product>>(CacheKeys.Product.AllProducts);

        if (cachedProducts != null && cachedProducts.Count() > 0)
            return new SuccessDataResult<List<Product>>(cachedProducts, ResultMessages.Success.ProductsInfoReceive);
        else
        {
            var products = await _productDal.GetAllAsync();
            if (products == null || !products.Any())
            {
                return new ErrorDataResult<List<Product>>(ResultMessages.Error.ProductsNotFound);
            }

            var expirationTime = DateTimeOffset.Now.AddMinutes(5);
            _cacheService.SetData<List<Product>>(CacheKeys.Product.AllProducts, products, expirationTime);

            return new SuccessDataResult<List<Product>>(products, ResultMessages.Success.ProductsInfoReceive);
        }
    }

    public async Task<IDataResult<Product>> GetByIdAsync(int id)
    {
        var product = await _productDal.GetAsync(p => p.Id == id);
        if (product == null)
        {
            return new ErrorDataResult<Product>(ResultMessages.Error.ProductNotFound);
        }
        return new SuccessDataResult<Product>(product, ResultMessages.Success.ProductInfoReceive);
    }

    public async Task<IResult> UpdateAsync(Product product)
    {
        var validationResult = _validator.Validate(product);
        if (!validationResult.IsValid)
        {
            var messages = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            return new ErrorResult(messages);
        }
        await _productDal.UpdateAsync(product);
        _cacheService.RemoveData(CacheKeys.Product.AllProducts);
        return new SuccessResult(ResultMessages.Success.ProductUpdate);
    }
}
