using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TmoTask.Application.Branch;
using TmoTask.Application.Performance;

namespace TmoTask.Application
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds application level services into the service collection 
        /// </summary>
        /// <param name="services">the source service collection</param>
        /// <param name="configuration">application configuration</param>
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IBranchService, BranchService>();
            services.AddSingleton<IPerformanceService, PerformanceService>();
        }
    }
}
