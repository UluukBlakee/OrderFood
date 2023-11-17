using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OrderFood.Models
{
    public class OrdFoodContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<User> Users { get; set; }
        public OrdFoodContext(DbContextOptions<OrdFoodContext> options) : base(options) { }
    }
}
