
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;
using WebApplication6.Models;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ICartServices _cartServices;

        public CartController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, ICartServices cartServices)
        {
            _context = context;
            _userManager = userManager;
            _cartServices = cartServices;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var items = await _cartServices.GetCartItemsAsync(user.Id);

            ViewBag.UserId = user.Id;

            if (items != null) return View(items);

            return View();
        }

        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var user = await _userManager.GetUserAsync(User);

            var product = _context.Products.Find(productId);
            if (product == null) { return NotFound(); }
            if (product.Stock > quantity)
            {
                await _cartServices.AddToCartAsync(user.Id, productId, quantity);
                product.Stock -= quantity;
                await _context.SaveChangesAsync();
            }
            else
            {
                ViewBag.Message = "محصول موجود نمیباشد";
            }


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            await _cartServices.RemoveFromCartAsync(cartItemId);
            return View("Index");
        }

        public async Task<IActionResult> ClearCart(string userId)
        {
            await _cartServices.ClearCartAsync(userId);
            return View("Index");
        }

    }



}
