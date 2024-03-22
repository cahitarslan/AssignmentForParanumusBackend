using Business.Abstract;
using Business.Concrete;
using Business.Services;
using Business.Utilities.Consts;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using Moq;
using System.Linq.Expressions;

namespace Tests;

public class ProductServiceTests
{
    private Mock<IProductDal> _productDalMock;
    private Mock<IValidator<Product>> _validatorMock;
    private Mock<ICacheService> _cacheServiceMock;
    private IProductService _productService;

    [SetUp]
    public void Setup()
    {
        _productDalMock = new Mock<IProductDal>();
        _validatorMock = new Mock<IValidator<Product>>();
        _cacheServiceMock = new Mock<ICacheService>();
        _productService = new ProductManager(_productDalMock.Object, _validatorMock.Object, _cacheServiceMock.Object);
    }

    [Test]
    public async Task AddAsync_ValidProduct_ReturnsSuccessResult()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Test Product", Price = 10, Description = "Test Description" };
        _validatorMock.Setup(v => v.Validate(product)).Returns(new FluentValidation.Results.ValidationResult());
        _cacheServiceMock.Setup(c => c.RemoveData(CacheKeys.Product.AllProducts));
        _productDalMock.Setup(p => p.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

        // Act
        var result = await _productService.AddAsync(product);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(ResultMessages.Success.ProductAdd, result.Message);
    }

    [Test]
    public async Task AddAsync_InvalidProduct_ReturnsErrorResult()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Test Product", Price = -10, Description = "Test Description" };
        _validatorMock.Setup(v => v.Validate(product)).Returns(new FluentValidation.Results.ValidationResult { Errors = { new FluentValidation.Results.ValidationFailure("Price", "Price must be greater than 0.") } });

        // Act
        var result = await _productService.AddAsync(product);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.Message.Contains("Price must be greater than 0."));
    }

    [Test]
    public async Task DeleteAsync_ValidProduct_ReturnsSuccessResult()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Test Product", Price = 10, Description = "Test Description" };
        _productDalMock.Setup(p => p.DeleteAsync(product)).Returns(Task.CompletedTask);
        _cacheServiceMock.Setup(c => c.RemoveData(CacheKeys.Product.AllProducts));

        // Act
        var result = await _productService.DeleteAsync(product);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(ResultMessages.Success.ProductDelete, result.Message);
    }

    [Test]
    public async Task GetAllAsync_CachedDataExists_ReturnsSuccessDataResult()
    {
        // Arrange
        var cachedProducts = new List<Product> { new Product { Id = 1, Name = "Test Product" } };
        _cacheServiceMock.Setup(c => c.GetData<List<Product>>(CacheKeys.Product.AllProducts)).Returns(cachedProducts);

        // Act
        var result = await _productService.GetAllAsync();

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(cachedProducts, result.Data);
    }

    [Test]
    public async Task GetByIdAsync_ExistingId_ReturnsSuccessDataResult()
    {
        // Arrange
        var productId = 1;
        var product = new Product { Id = productId, Name = "Test Product" };
        _productDalMock.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(product);

        // Act
        var result = await _productService.GetByIdAsync(productId);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(product, result.Data);
    }

    [Test]
    public async Task GetByIdAsync_NonExistingId_ReturnsErrorDataResult()
    {
        // Arrange
        var productId = 1;
        _productDalMock.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(() => null);

        // Act
        var result = await _productService.GetByIdAsync(productId);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.Message.Contains("Product not found"));
    }

    [Test]
    public async Task UpdateAsync_ValidProduct_ReturnsSuccessResult()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Test Product", Price = 10, Description = "Test Description" };
        _validatorMock.Setup(v => v.Validate(product)).Returns(new FluentValidation.Results.ValidationResult());
        _productDalMock.Setup(p => p.UpdateAsync(product)).Returns(Task.CompletedTask);
        _cacheServiceMock.Setup(c => c.RemoveData(CacheKeys.Product.AllProducts));

        // Act
        var result = await _productService.UpdateAsync(product);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(ResultMessages.Success.ProductUpdate, result.Message);
    }

    [Test]
    public async Task UpdateAsync_InvalidProduct_ReturnsErrorResult()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Test Product", Price = -10, Description = "Test Description" };
        _validatorMock.Setup(v => v.Validate(product)).Returns(new FluentValidation.Results.ValidationResult { Errors = { new FluentValidation.Results.ValidationFailure("Price", "Price must be greater than 0.") } });

        // Act
        var result = await _productService.UpdateAsync(product);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.Message.Contains("Price must be greater than 0."));
    }
}
