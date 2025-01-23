namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarter(new StaticDependencyContextCatalog(typeof(Program).Assembly));
            services.AddExceptionHandler<CommonExceptionHandler>();

            var healthChekcks = services.AddHealthChecks();

            var connectionString = configuration.GetConnectionString("Database");
            if(!string.IsNullOrEmpty(connectionString))
            {
                healthChekcks.AddSqlServer(connectionString);
            }   

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();
            app.UseExceptionHandler(options => { });
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }
    }
}
