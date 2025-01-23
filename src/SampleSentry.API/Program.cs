using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleSentry.API.Common.Extensions;
using SampleSentry.API.Context;
using SampleSentry.API.Entities;
using SampleSentry.API.Features.ApplicationUser.Validators;
using SampleSentry.API.Middleware;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention();
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>().AddErrorDescriber<CustomIdentityValidator>()
    .AddDefaultTokenProviders();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDependencyInjection(builder.Configuration);

builder.WebHost.UseSentry(o =>
{
    o.Dsn = builder.Configuration["Sentry"];
    o.TracesSampleRate = 1.0;
    o.AttachStacktrace = true;
    o.SendDefaultPii = true;
    o.Environment = builder.Environment.EnvironmentName;
    o.MinimumEventLevel = LogLevel.Warning;
    o.MinimumBreadcrumbLevel = LogLevel.Warning;
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Sample Sentry API";
    });
}

app.UseApplication();

app.UseSentryTracing();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
