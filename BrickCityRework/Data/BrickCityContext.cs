using BrickCityRework.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace BrickCityRework.Data
{
    public class BrickCityContext : DbContext
    {
        public BrickCityContext(DbContextOptions<BrickCityContext> options) : base(options)
        {
        }

        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Consumption> Consumptions { get; set; }
        public DbSet<Msrmnt> Msrmnts { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<FileModel> Files { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consumer>().ToTable("Consumer");
            modelBuilder.Entity<Consumption>().ToTable("Consumption");
            modelBuilder.Entity<Weather>().ToTable("Weather");
            modelBuilder.Entity<Price>().ToTable("Price");
            modelBuilder.Entity<Msrmnt>().ToTable("Msrmnt");
            modelBuilder.Entity<FileModel>().ToTable("File");
        }
    }
}
