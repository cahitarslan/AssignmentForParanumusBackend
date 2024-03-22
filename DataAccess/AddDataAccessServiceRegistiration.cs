using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class AddDataAccessServiceRegistiration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddSingleton<IProductDal, ImProductDal>();

        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ParanusmusConnectionString")));
        services.AddScoped<IProductDal, EfProductDal>();
        services.AddScoped<IUserDal, EfUserDal>();
        services.AddScoped<IOrderDal, EfOrderDal>();

        return services;
    }
}
