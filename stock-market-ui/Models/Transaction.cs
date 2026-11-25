
namespace StockMarket.API.Models
{
  public class Transaction
  {
    public int TransactionId { get; set; }

    public int PortfolioId { get; set; }

    public int StockId { get; set; }

    public string TransactionType { get; set; } = "BUY";

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    public DateTime TransactionDate { get; set; }
        = DateTime.UtcNow;

    public Portfolio? Portfolio { get; set; }

    public Stock? Stock { get; set; }
  }
}
