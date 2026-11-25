
namespace StockMarket.API.Models
{
  public class AppUser
  {
    public int UserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    public ICollection<Watchlist> Watchlists { get; set; }
        = new List<Watchlist>();

    public ICollection<Portfolio> Portfolios { get; set; }
        = new List<Portfolio>();
  }
}
