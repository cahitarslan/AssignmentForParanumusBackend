using Business.Abstract;
using Business.Utilities.Consts;
using Business.Utilities.Results;
using Business.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;

namespace Business.Concrete;

public class ProductManager : IProductService
{
    private readonly IProductDal _productDal;
    private readonly IValidator<Product> _validator;

    public ProductManager(IProductDal productDal, IValidator<Product> validator)
    {
        _productDal = productDal;
        _validator = validator;
    }


    public async Task<IResult> AddAsync(Product product)
    {
        try
        {
            var validationResult = _validator.Validate(product);
            if (!validationResult.IsValid)
            {
                var messages = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
                return new ErrorResult(messages);
            }
            await _productDal.AddAsync(product);
            return new SuccessResult(ResultMessages.Success.ProductAdd);
        }
        catch (Exception ex)
        {
            return new ErrorResult($"{ResultMessages.Error.ProductAddServer}\n{ex.Message}");
        }
    }

    public async Task<IResult> DeleteAsync(Product product)
    {
        try
        {
            await _productDal.DeleteAsync(product);
            return new SuccessResult(ResultMessages.Success.ProductDelete);
        }
        catch (Exception ex)
        {
            return new ErrorResult($"{ResultMessages.Error.ProductDeleteServer}\n{ex.Message}");
        }
    }

    public async Task<IDataResult<List<Product>>> GetAllAsync()
    {
        try
        {
            var products = await _productDal.GetAllAsync();
            if (products == null || !products.Any())
            {
                return new ErrorDataResult<List<Product>>(ResultMessages.Error.ProductsNotFound);
            }
            return new SuccessDataResult<List<Product>>(products, ResultMessages.Success.ProductsInfoReceive);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<Product>>($"{ResultMessages.Error.ProductGetAllServer}\n{ex.Message}");
        }
    }

    public async Task<IDataResult<Product>> GetByIdAsync(int id)
    {
        try
        {
            var product = await _productDal.GetAsync(p => p.Id == id);
            if (product == null)
            {
                return new ErrorDataResult<Product>(ResultMessages.Error.ProductNotFound);
            }
            return new SuccessDataResult<Product>(product, ResultMessages.Success.ProductInfoReceive);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<Product>($"{ResultMessages.Error.ProductGetServer}\n{ex.Message}");
        }
    }

    public async Task<IResult> UpdateAsync(Product product)
    {
        try
        {
            var validationResult = _validator.Validate(product);
            if (!validationResult.IsValid)
            {
                var messages = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
                return new ErrorResult(messages);
            }
            await _productDal.UpdateAsync(product);
            return new SuccessResult(ResultMessages.Success.ProductUpdate);
        }
        catch (Exception ex)
        {
            return new ErrorResult($"{ResultMessages.Error.ProductUpdateServer}\n{ex.Message}");
        }
    }
}
