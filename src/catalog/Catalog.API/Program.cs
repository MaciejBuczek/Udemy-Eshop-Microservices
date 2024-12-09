var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services.AddCarter(new StaticDependencyContextCatalog(typeof(Program).Assembly));
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
builder.Services.AddExceptionHandler<CommonExceptionHandler>();

var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();