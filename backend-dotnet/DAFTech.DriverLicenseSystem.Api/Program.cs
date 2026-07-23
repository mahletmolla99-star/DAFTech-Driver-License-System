using DAFTech.DriverLicenseSystem.Api.Data;
using DAFTech.DriverLicenseSystem.Api.Repositories;
using DAFTech.DriverLicenseSystem.Api.Services;
using DAFTech.DriverLicenseSystem.Api.Helpers;
using DAFTech.DriverLicenseSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddDbContext<DriverLicenseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<JwtHelper>();

var app = builder.Build();

app.UseCors("AllowAll");

// Seed database with test user
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DriverLicenseDbContext>();
    context.Database.Migrate();
    
    if (!context.Users.Any(u => u.Username == "testuser"))
    {
        var testUser = new User
        {
            Username = "testuser",
            PasswordHash = UserRepository.HashPassword("Test@123"),
            CreatedDate = DateTime.Now,
            Status = "Active"
        };
        context.Users.Add(testUser);
        context.SaveChanges();
    }
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<DAFTech.DriverLicenseSystem.Api.Middleware.GlobalExceptionMiddleware>();
app.Run();
