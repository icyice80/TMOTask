namespace TmoTask.Support
{

    /// <summary>
    /// Provides extension methods for <see cref="WebApplication"/>
    /// </summary>
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Configure the application pipeline
        /// </summary>
        /// <param name="app">the application to configure the pipeline for</param>
        public static void Configure(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseCors("AllowAnyOrigin");
            app.MapControllers();
        }
    }
}
