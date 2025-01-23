namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddCarter(new StaticDependencyContextCatalog(typeof(Program).Assembly));
            services.AddExceptionHandler<CommonExceptionHandler>();
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();
            app.UseExceptionHandler(options => { });
            return app;
        }
    }
}
