using BuySell.Business.Application.Abstraction.Enums;
using BuySell.Business.Application.Abstraction.Storage;
using BuySell.Business.Application.Repositories;
using BuySell.Business.Application.Services;
using BuySell.Business.Application.Services.Storage;
using BuySell.Business.Application.Services.Storage.Local;
using BuySell.Business.Domain.Entities;
using BuySell.CommonModels.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Application
{
    public static class ApplicationServiceBusinessRegistration
    {
        public static void AddApplicationServicesBusiness(this IServiceCollection services)
        {
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            
            services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            services.AddScoped<IStorageService, StorageService>();
            //services.AddScoped<IFileReadRepository, FileReadRepository>();

        }

        public static void AddStorage<T>(this IServiceCollection services) where T: class, IStorage
        {
            services.AddScoped<IStorage, T>();
        }

        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    //serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:

                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
