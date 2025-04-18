using EticaretAPI.Application.Abstractions;
using EticaretAPI.Application.Abstractions.Services;
using EticaretAPI.Application.Repositories;
using EticaretAPI.Domain.Entities.Identity;
using EticaretAPI.Persistance.Contexts;
using EticaretAPI.Persistance.Repositories;
using EticaretAPI.Persistance.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddSingleton<IProductService, IProductService>();
        services.AddDbContext<ETicaretAPIDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("FerqiOlmayanConnectionString"));
        });
        services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ETicaretAPIDbContext>();

        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();

        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();


        services.AddScoped<IOrderReadRepository, OrderReadRepository>();

        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

        services.AddScoped<IProductReadRepository,ProductReadRepository>();

        services.AddScoped<IProductWriteRepository,ProductWriteRepository>();



        services.AddScoped<IProductImageFileReadRepository,ProductImageFileReadRepository>();

        services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();


        services.AddScoped<IInvoiceFileReadRepository,InvoiceFileReadRepository>();
        services.AddScoped<IInvoiceFileWriteRepository,InvoiceFileWriteRepository>();

        services.AddScoped<IFileReadRepository,FileReadRepository>();
        services.AddScoped<IFileWriteRepository,FileWriteRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();


    }
}
