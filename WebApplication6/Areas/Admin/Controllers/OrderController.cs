using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    public class OrderController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public OrderController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }


        public async Task<IActionResult> AddToOrders(List<OrderItem> orderItems)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var cartItems = _context.CartItems.Include(p => p.Product).Where(c => c.UserId == user.Id).ToList();

            var order = new Order()
            {
                UserId = user.Id,
                IsPaid = true,
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price,
                   

                }).ToList()

            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var orderItems = await _context.OrderItems.
                Include(o => o.Order)
                .Include(p => p.Product)
                .Where(o => o.Order.UserId == userId)
                .ToListAsync();

            ViewBag.FullName = _userManager.GetUserName(User);


            return View(orderItems);
        }
    }
}
