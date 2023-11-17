using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderFood.Models;

namespace OrderFood.Controllers
{
    [Authorize]
    public class CafesController : Controller
    {
        private readonly OrdFoodContext _context;
        public CafesController(OrdFoodContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Cafe> cafes = await _context.Cafes.ToListAsync();
            return View(cafes);
        }
        public async Task<IActionResult> Details(int id)
        {
            Cafe cafe = await _context.Cafes.Include(c => c.Dishes).FirstOrDefaultAsync(c => c.Id == id);
            return View(cafe);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cafe cafe)
        {
            if (cafe != null)
            {
                cafe.Image ??= "https://png.pngtree.com/png-vector/20191119/ourmid/pngtree-small-cafe-vector-illustration-with-flat-design-isolated-on-white-background-png-image_2000519.jpg";
                await _context.Cafes.AddAsync(cafe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cafe);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Cafe cafe = await _context.Cafes.Include(c => c.Dishes).FirstOrDefaultAsync(c => c.Id == id);
            return View(cafe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Cafe cafe)
        {
            if (cafe != null)
            {
                _context.Cafes.Remove(cafe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cafe);
        }

    }
}
