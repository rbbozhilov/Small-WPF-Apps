using Gym.Data.Models;
using System.Data.Entity;

namespace Gym.Data
{
    public class GymDbContext : DbContext
    {
        public GymDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Client> Clients { get; set; }
    }
}
