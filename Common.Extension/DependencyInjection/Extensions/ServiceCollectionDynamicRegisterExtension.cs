using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.DependencyInjection.Extensions
{
    public enum FilterNameType
    {
        Equal,
        Prefix,
        Suffix,
        Contains
    }
    public static class ServiceCollectionDynamicRegisterExtension
    {
        #region Enums
        enum DependecyType
        {
            Interface,
            Class,
        }

        #endregion

        #region Public Methods 
        public static IServiceCollection AddDependeciesDynamic(this IServiceCollection services,
                                                        ServiceLifetime serviceLifetime, string namespaceName,
                                                        string filterName, FilterNameType filterNameType)
        {
            var interfaces = GetAssemblyNamedTypes(namespaceName, DependecyType.Interface, filterName, filterNameType);
            var classes = GetAssemblyNamedTypes(namespaceName, DependecyType.Class, filterName, filterNameType);

            services.RegisterTypes(serviceLifetime, interfaces, classes);

            return services;
        }

        public static IServiceCollection AddDependeciesDynamic(this IServiceCollection services,
                                                       ServiceLifetime serviceLifetime, string namespaceName,
                                                       Type baseType)
        {
            var interfaces = GetAssemblyTypeChilds(namespaceName, DependecyType.Interface, baseType);
            var classes = GetAssemblyTypeChilds(namespaceName, DependecyType.Class, baseType);

            services.RegisterTypes(serviceLifetime, interfaces, classes);

            return services;
        }


        #endregion

        #region Helper Methods
        private static List<Type> GetAssemblyNamedTypes(string namespaceName, DependecyType dependecyType,
                                                       string filterName, FilterNameType filterNameType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                            .Where(a => a.FullName.Contains(namespaceName))
                            .FirstOrDefault()
                            .GetTypes()
                            .AsEnumerable()
                            .FilterByDependecyType(dependecyType)
                            .FilterByName(filterName, filterNameType)
                            .ToList();
        }
        private static List<Type> GetAssemblyTypeChilds(string namespaceName, DependecyType dependecyType, Type type)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                            .Where(a => a.FullName.Contains(namespaceName))
                            .FirstOrDefault()
                            .GetTypes()
                            .AsEnumerable()
                            .FilterChildsByDependecyType(dependecyType, type)
                            .ToList();
        }



        private static IServiceCollection RegisterTypes(this IServiceCollection services,
                                                        ServiceLifetime serviceLifetime, List<Type> interfaces,
                                                        List<Type> classes)
        {
            interfaces.ForEach(delegate (Type i)
            {
                var nameComparer = i.Name[1..].ToString();
                var implement = classes.FirstOrDefault(c => c.Name == nameComparer);

                if (implement != null)
                {
                    services.AddToServices(serviceLifetime, i, implement);
                }
            });

            return services;
        }

        private static IServiceCollection AddToServices(this IServiceCollection services,
                                                        ServiceLifetime serviceLifetime, Type interfaceType,
                                                        Type implement)
        {
            return serviceLifetime switch
            {
                ServiceLifetime.Singleton => services.AddSingleton(interfaceType, implement),
                ServiceLifetime.Scoped => services.AddScoped(interfaceType, implement),
                ServiceLifetime.Transient => services.AddTransient(interfaceType, implement),
                _ => services,
            };
        }
        private static IEnumerable<Type> FilterByName(this IEnumerable<Type> types, string filterWord,
                                                      FilterNameType filterNameType)
        {
            if (!string.IsNullOrEmpty(filterWord))
            {
                types = filterNameType switch
                {
                    FilterNameType.Equal => types.Where(t => t.Name == filterWord),
                    FilterNameType.Prefix => types.Where(t => (t.IsInterface
                                                                 && t.Name[1..]
                                                                     .ToLower()
                                                                     .StartsWith(filterWord.ToLower()))
                                                                 || t.Name.ToLower()
                                                                 .StartsWith(filterWord.ToLower())),
                    FilterNameType.Suffix => types.Where(t => t.Name.ToLower()
                                                                    .EndsWith(filterWord.ToLower())),
                    FilterNameType.Contains => types.Where(t => t.Name.Contains(filterWord.ToLower())),
                    _ => types.Where(t => t.Name.Contains(filterWord)),
                };
            }

            return types;
        }

        private static IEnumerable<Type> FilterByDependecyType(this IEnumerable<Type> types,
                                                               DependecyType dependecyType)
        {
            return dependecyType switch
            {
                DependecyType.Interface => types = types.Where(t => t.IsInterface || (t.IsInterface && t.ContainsGenericParameters)),
                DependecyType.Class => types = types.Where(t => t.IsClass || (t.IsClass && t.ContainsGenericParameters)),
                _ => types,
            };
        }

        private static IEnumerable<Type> FilterChildsByDependecyType(this IEnumerable<Type> types, DependecyType dependecyType, Type type)
        {
            return dependecyType switch
            {
                DependecyType.Interface => types = types.Where(x => type.IsAssignableFrom(x) && x.IsInterface),
                DependecyType.Class => types = types.Where(x => type.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract),
                _ => types
            };

        }
        #endregion
    }
}
