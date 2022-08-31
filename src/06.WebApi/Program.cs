using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;
using Zeta.CodebaseExpress.Application;
using Zeta.CodebaseExpress.Infrastructure;
using Zeta.CodebaseExpress.Infrastructure.Logging;
using Zeta.CodebaseExpress.Infrastructure.Persistence;
using Zeta.CodebaseExpress.Shared;
using Zeta.CodebaseExpress.Shared.Common.Constants;
using Zeta.CodebaseExpress.WebApi.Common.Filters.ApiException;
using Zeta.CodebaseExpress.WebApi.Services;

Console.WriteLine($"Starting {CommonValueFor.EntryAssemblySimpleName}...");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLoggingService();
builder.Services.AddShared();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebApi(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(options => options.Filters.Add(new ApiExceptionFilterAttribute()))
    .AddFluentValidation()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

using var scope = app.Services.CreateScope();
await scope.ServiceProvider.ApplyDatabaseMigrationAsync<Program>();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Running {AssemblyName}", CommonValueFor.EntryAssemblySimpleName);

if (app.Environment.IsEnvironment(EnvironmentNames.Local) || app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseWebApi(builder.Configuration);
app.MapControllers();
app.Run();
