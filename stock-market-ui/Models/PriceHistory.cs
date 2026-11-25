
namespace StockMarket.API.Models
{
  public class PriceHistory
  {
    public int PriceHistoryId { get; set; }

    public int StockId { get; set; }

    public DateTime Date { get; set; }

    public decimal Open { get; set; }

    public decimal High { get; set; }

    public decimal Low { get; set; }

    public decimal Close { get; set; }

    public long Volume { get; set; }

    public Stock? Stock { get; set; }
  }
}
