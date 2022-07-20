using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence.EF.IUnitOfWorks;
using Persistence.EF.Repositories.Packages;
using Persistence.EF.Repositories.ProductCatalogue.DataQueries;
using ProductCatalogue.Application.Common.Behaviours;
using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.Common.Messaging;
using ProductCatalogue.Application.ProductCatalogue.IDataQueries;
using ProductCatalogue.Application.ProductCatalogue.IRepositories;
using ProductCatalogue.Persistence.EF;
using ProductCatalogue.Persistence.EF.Repositories;
using System;
using System.Linq;
using System.Reflection;

namespace ProductCatalogue.Configuration.WebUI
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
        public static IServiceCollection AddWebDependencies(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddValidatorsFromAssembly(AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.Contains("ProductCatalogue.Application"))
                            .FirstOrDefault());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.Contains("ProductCatalogue.Application"))
                            .FirstOrDefault());
            services.AddTransient(typeof(IDataQuery<>), typeof(DataQuery<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IRequestPipeline<,>), typeof(RequestPipeline<,>));
            services.AddTransient(typeof(IRequestPipeline<,>), typeof(LoggerPipline<,>));
            services.AddTransient<IProductDataQuery, ProductDataQuery>();
         
            return services;
        }
    }
}