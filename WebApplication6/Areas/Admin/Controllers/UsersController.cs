using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.Areas.Admin.Models;
using WebApplication6.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication6.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            _userManager = userManager;
            _roleManager = roleManager;

        }
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userWithRoles = new List<UserWithRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userWithRoles.Add(new UserWithRolesViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = roles
                });
            }


            return View(userWithRoles);
        }

        public async Task<IActionResult> ManageRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return NotFound();

            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = roles.Select(role => new ManageRolesViewModel
            {
                RoleName = role.Name,
                IsSelected = userRoles.Contains(role.Name)
            }).ToList();

            ViewBag.UserId=user.Id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageRoles(string id, List<ManageRolesViewModel> model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = model.Where(m => m.IsSelected).Select(m => m.RoleName).ToList();

            var result = await _userManager.RemoveFromRolesAsync(user,currentRoles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "خطا در حذف نقش های قبلی");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user,selectedRoles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("","خطا در افزودن نقش های جدید");
                return View(model);
            }

            return RedirectToAction(nameof(Index));   
        }

    }
}
