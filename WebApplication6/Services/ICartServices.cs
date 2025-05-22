using WebApplication6.Models;

namespace WebApplication6.Services
{
    public interface ICartServices
    {
        Task<List<CartItem>> GetCartItemsAsync(string userId);
        Task AddToCartAsync(string userId,int productId, int quantity);
        Task RemoveFromCartAsync(int cartItemId);
        Task ClearCartAsync(string userId);
        
        
    }
}
