
namespace StockMarket.API.Models
{
  public class WatchlistItem
  {
    public int WatchlistItemId { get; set; }

    public int WatchlistId { get; set; }

    public int StockId { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    public Watchlist? Watchlist { get; set; }

    public Stock? Stock { get; set; }
  }
}
