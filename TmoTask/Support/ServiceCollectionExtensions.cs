using Serilog;

namespace TmoTask.Support
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds application host services into the service collection
        /// </summary>
        /// <param name="builder"></param>
        public static void AddHostServices(this WebApplicationBuilder builder)
        {
            //setup logging for the application
            builder.Logging.ClearProviders();
            builder.Host.UseSerilog((ctx, services, lc) => lc
                .ReadFrom.Configuration(ctx.Configuration)
                .ReadFrom.Services(services)
            );

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    c => c.WithOrigins("*") // Any urls
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
        }
    }
}
