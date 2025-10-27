using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FullStackFinalProject.Api.Models;
using FullStackFinalProject.Api.Data;

namespace FullStackFinalProject.Api.Services
{
    public class PasswordHashing
    {
        private readonly AppDbContext _context;

        public PasswordHashing(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
