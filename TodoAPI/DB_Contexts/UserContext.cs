using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.DB_Contexts
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
    }
}
