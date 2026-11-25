using StockMarket.API.Models;

namespace StockMarket.API.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(AppUser user);
    }
}