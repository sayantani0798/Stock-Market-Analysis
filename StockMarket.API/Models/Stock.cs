
namespace StockMarket.API.Models
{
    public class Stock
    {
        public int StockId { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public string Exchange { get; set; } = "NSE";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<WatchlistItem> WatchlistItems { get; set; }
            = new List<WatchlistItem>();

        public ICollection<PriceHistory> PriceHistories { get; set; }
            = new List<PriceHistory>();
    }
}
