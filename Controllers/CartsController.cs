using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderFood.Models;

namespace OrderFood.Controllers
{
    public class CartsController : Controller
    {
        private readonly OrdFoodContext _context;
        public CartsController(OrdFoodContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> AddCart(int dishId)
        {
            Dish dish = await _context.Dishes.Include(d => d.Cafes).FirstOrDefaultAsync(d => d.Id == dishId);
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            Cart cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == user.Id && c.DishId == dishId);
            if (cart != null)
            {
                cart.Quantity += 1;
                _context.Carts.Update(cart);
                await _context.SaveChangesAsync();
                return Json(cart);
            }
            else
            {
                Cart newCart = new Cart()
                {
                    DishId = dish.Id,
                    UserId = user.Id,
                    Quantity = 1
                };
                await _context.Carts.AddAsync(newCart);
                await _context.SaveChangesAsync();
                return Json(newCart);
            }
        }
        public async Task<IActionResult> RemoveCart(int cartId)
        {
            Cart cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartId);
            if (cart != null)
            {
                if (cart.Quantity > 1)
                {
                    cart.Quantity -= 1;
                    _context.Carts.Update(cart);
                    await _context.SaveChangesAsync();
                    return Json(cart);
                }
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
            return Json(cart);
        }
        public async Task<IActionResult> GetCart(int cafeId)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            List<Dish> dishes = await _context.Dishes.Where(d => d.CafesId == cafeId).ToListAsync();
            List<Cart> carts = new List<Cart>();
            foreach (var dish in dishes)
            {
                Cart cart = await _context.Carts.Include(c => c.Dish).FirstOrDefaultAsync(c => c.UserId == user.Id && c.DishId == dish.Id);
                carts.Add(cart);
            }
            
            return Json(carts);
        }
    }
}
