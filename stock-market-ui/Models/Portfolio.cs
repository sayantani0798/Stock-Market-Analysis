
namespace StockMarket.API.Models
{
  public class Portfolio
  {
    public int PortfolioId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = "My Portfolio";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public AppUser? User { get; set; }

    public ICollection<Transaction> Transactions { get; set; }
        = new List<Transaction>();
  }
}
