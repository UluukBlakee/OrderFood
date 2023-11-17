using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderFood.Models;
using OrderFood.ViewModels;
using System.Data;

namespace OrderFood.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly OrdFoodContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserController(OrdFoodContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<User> users = await _context.Users.ToListAsync();
            ViewBag.UserManager = _userManager;
            return View(users);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Пользователь с такой почтой уже зарегистрирован.");
                    return View(model);
                }
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GiveRole(int id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "admin");
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
