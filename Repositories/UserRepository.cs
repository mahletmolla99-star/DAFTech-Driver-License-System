using DAFTech.DriverLicenseSystem.Api.Data;
using DAFTech.DriverLicenseSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DAFTech.DriverLicenseSystem.Api.Repositories
{
    public class UserRepository
    {
        private readonly DriverLicenseDbContext _context;

        public UserRepository(DriverLicenseDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
    }
}
