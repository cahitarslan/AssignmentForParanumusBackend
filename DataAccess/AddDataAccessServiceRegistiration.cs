using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class AddDataAccessServiceRegistiration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services.AddSingleton<IProductDal, ImProductDal>();

        return services;
    }
}
