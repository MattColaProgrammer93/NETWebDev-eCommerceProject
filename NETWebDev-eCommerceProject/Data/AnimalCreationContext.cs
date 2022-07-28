using Microsoft.EntityFrameworkCore;
using NETWebDev_eCommerceProject.Models;

namespace NETWebDev_eCommerceProject.Data
{
    public class AnimalCreationContext : DbContext
    {
        public AnimalCreationContext(DbContextOptions<AnimalCreationContext> options) 
            : base(options)
        {

        }

        public DbSet<Animal> Animals { get; set; }
    }
}
