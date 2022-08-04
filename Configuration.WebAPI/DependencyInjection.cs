using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalogue.Application.Common.Behaviours;
using ProductCatalogue.Application.Common.Messaging;
using ProductCatalogue.Persistence.EF;
using System;
using System.Linq;
using System.Reflection;

namespace Configuration.WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            return services;
        }
        public static IServiceCollection AddWebAPIDependencies(this IServiceCollection services)
        {
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<ICartRepository, CartRepository>();
            //services.AddScoped<ICartItemRepository, CartItemRepository>();

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.Contains("ProductCatalogue.Application"))
                            .FirstOrDefault());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped(typeof(IRequestPipeline<,>), typeof(RequestPipeline<,>));

            return services;
        }
    }
}
