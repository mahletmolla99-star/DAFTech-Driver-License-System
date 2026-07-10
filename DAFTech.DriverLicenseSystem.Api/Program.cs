using DAFTech.DriverLicenseSystem.Api.Data;
using DAFTech.DriverLicenseSystem.Api.Repositories;
using DAFTech.DriverLicenseSystem.Api.Services;
using DAFTech.DriverLicenseSystem.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add services to the container
builder.Services.AddControllers();

// Register DbContext
builder.Services.AddDbContext<DriverLicenseDbContext>();

// Register Repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<DriverRepository>();
builder.Services.AddScoped<VerificationLogRepository>();

// Register Services
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<DriverService>();
builder.Services.AddScoped<VerificationService>();

// Register JWT Helper
builder.Services.AddScoped<JwtHelper>();

var app = builder.Build();

// Use CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
