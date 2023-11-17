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
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(User user)
        {
            return View();
        }
    }
}
