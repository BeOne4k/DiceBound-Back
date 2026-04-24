namespace DiceBound.Persistence
{
    using DiceBound.Entity_s.Characters;
    using DiceBound.Entity_s.Gameplay;
    using DiceBound.Entity_s.Identity;
    using DiceBound.Entity_s.Items;
    using DiceBound.Entity_s.Payments;
    using Microsoft.EntityFrameworkCore;
    using DiceBound.Entity_s.Gameplay;

    public class DiceBoundDbContext : DbContext
    {
        public DiceBoundDbContext(DbContextOptions<DiceBoundDbContext> options)
            : base(options)
        {
        }

        // Identity
        public DbSet<User> Users => Set<User>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();

        // Characters
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<Race> Races => Set<Race>();

        // Items
        public DbSet<Item> Items => Set<Item>();
        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();

        // Gameplay
        public DbSet<Mission> Missions => Set<Mission>();
        public DbSet<Boss> Bosses => Set<Boss>();
        public DbSet<Enemy> Enemies => Set<Enemy>();
        public DbSet<CombatLog> CombatLogs => Set<CombatLog>();

        // Payments
        public DbSet<Payment> Payments => Set<Payment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DiceBoundDbContext).Assembly);
        }



    }
}
