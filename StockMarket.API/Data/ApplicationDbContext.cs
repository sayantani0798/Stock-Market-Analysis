
using Microsoft.EntityFrameworkCore;
using StockMarket.API.Models;

namespace StockMarket.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<WatchlistItem> WatchlistItems { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Stock>()
                .HasIndex(s => new { s.Symbol, s.Exchange })
                .IsUnique();

            modelBuilder.Entity<Watchlist>()
                .HasOne(w => w.User)
                .WithMany(u => u.Watchlists)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WatchlistItem>()
                .HasIndex(w => new { w.WatchlistId, w.StockId })
                .IsUnique();

            modelBuilder.Entity<WatchlistItem>()
                .HasOne(w => w.Watchlist)
                .WithMany(w => w.Items)
                .HasForeignKey(w => w.WatchlistId);

            modelBuilder.Entity<WatchlistItem>()
                .HasOne(w => w.Stock)
                .WithMany(s => s.WatchlistItems)
                .HasForeignKey(w => w.StockId);

            modelBuilder.Entity<Portfolio>()
                .HasOne(p => p.User)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Portfolio)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.PortfolioId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Stock)
                .WithMany()
                .HasForeignKey(t => t.StockId);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Price)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Quantity)
                .HasPrecision(18, 4);

            modelBuilder.Entity<PriceHistory>()
                .HasOne(p => p.Stock)
                .WithMany(s => s.PriceHistories)
                .HasForeignKey(p => p.StockId);

            modelBuilder.Entity<PriceHistory>()
                .HasIndex(p => new { p.StockId, p.Date })
                .IsUnique();

            modelBuilder.Entity<PriceHistory>()
                .Property(p => p.Close)
                .HasPrecision(18, 4);
        }
    }
}
