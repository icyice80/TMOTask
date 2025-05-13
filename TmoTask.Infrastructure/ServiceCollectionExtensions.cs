using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TmoTask.Application;
using TmoTask.Infrastructure.cache;
using TmoTask.Infrastructure.csv;

namespace TmoTask.Infrastructure
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Infrastructure level services into the service collection 
        /// </summary>
        /// <param name="services">the source service collection</param>
        /// <param name="configuration">application configuration</param>
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.Configure<AppSettingOptions>(configuration.GetSection(AppSettingOptions.Name));
            services.AddSingleton<IDataService, CsvDataService>();
            services.AddSingleton<IDataCacheService, DataCacheService>();
            services.AddHostedService<CsvFileWatcherService>();
        }
    }
}
