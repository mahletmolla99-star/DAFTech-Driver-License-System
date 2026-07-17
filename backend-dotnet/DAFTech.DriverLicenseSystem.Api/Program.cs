using DAFTech.DriverLicenseSystem.Api.Data;
using DAFTech.DriverLicenseSystem.Api.Repositories;
using DAFTech.DriverLicenseSystem.Api.Services;
using DAFTech.DriverLicenseSystem.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<JwtHelper>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<DAFTech.DriverLicenseSystem.Api.Middleware.GlobalExceptionMiddleware>();

app.Run();
