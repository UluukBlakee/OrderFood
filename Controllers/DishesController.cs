using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderFood.Models;
using System.Data;

namespace OrderFood.Controllers
{
    [Authorize]
    public class DishesController : Controller
    {
        private readonly OrdFoodContext _context;
        public DishesController(OrdFoodContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create(int cafeId)
        {
            ViewBag.CafeId = cafeId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dish dish)
        {
            if (dish != null)
            {
                await _context.Dishes.AddAsync(dish);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Cafes", new { id = dish.CafesId });
            }
            return View(dish);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Dish dish = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == id);
            return View(dish);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Dish dish = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == id);
            if (dish != null)
            {
                _context.Dishes.Remove(dish);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Cafes", new { id = dish.CafesId });
            }
            return View(dish);
        }
    }
}
