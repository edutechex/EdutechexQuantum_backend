using Microsoft.EntityFrameworkCore;


namespace EdutechexQuantum.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Service> Service { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<CareerOppertunity> CareerOppertunity { get;set; }
        public DbSet<Visitors> Visitors { get; set; }
    }
}