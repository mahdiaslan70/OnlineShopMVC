using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Services
{
    public class CartServices : ICartServices
    {
        private readonly ApplicationDbContext _context;

        public CartServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }

            else
            {
                cartItem = new CartItem()
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = quantity
                };

                _context.CartItems.Add(cartItem);
            }

            
            await _context.SaveChangesAsync();
        }

        public async Task ClearCartAsync(string userId)
        {
            var cartItems = _context.CartItems.Where(c => c.UserId == userId).ToList();
            _context.CartItems.RemoveRange(cartItems);

            await _context.SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetCartItemsAsync(string userId)
        {
            var cartItems = await _context.CartItems.Where(c => c.UserId == userId).Include(p => p.Product).ToListAsync();
            return cartItems;
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity -= 1;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }

                await _context.SaveChangesAsync();

            }

        }
    }
}
