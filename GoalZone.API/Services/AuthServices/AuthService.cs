using GoalZone.API.Data;
using GoalZone.API.Services.AuthServices;
using Microsoft.EntityFrameworkCore;

namespace GoalZone.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly GoalZoneDbContext _context;
        public AuthService(GoalZoneDbContext context) => _context = context;

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return false;
            var valid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (valid) { user.LastLoginAt = DateTime.UtcNow; await _context.SaveChangesAsync(); }
            return valid;
        }
    }
}