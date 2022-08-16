using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EF;
using Persistence.EF.DataQueries;
using Persistence.EF.DataQueries.ProductCatalogue;
using ProductCatalogue.Application.Common.Behaviours;
using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.Common.Messaging;
using ProductCatalogue.Application.ProductCatalogue.IDataQueries;
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

            services.AddDbContext<CatalogueDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(CatalogueDbContext).Assembly.FullName)));

            return services;
        }
        public static IServiceCollection AddWebAPIDependencies(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.Contains("ProductCatalogue.Application"))
                            .FirstOrDefault());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped(typeof(IRequestPipeline<,>), typeof(RequestPipeline<,>));
           
            services.AddTransient(typeof(IDataQuery<>), typeof(DataQuery<>));
            services.AddTransient<IProductDataQuery, ProductDataQuery>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
