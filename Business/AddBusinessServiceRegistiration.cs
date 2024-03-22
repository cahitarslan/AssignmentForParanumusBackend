using Business.Abstract;
using Business.Concrete;
using Business.Utilities.Validators;
using Entities.Concrete;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class AddBusinessServiceRegistiration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<IUserService, UserManager>();
        services.AddScoped<IOrderService, OrderManager>();

        services.AddTransient<IValidator<Product>, ProductValidator>();

        return services;
    }
}
