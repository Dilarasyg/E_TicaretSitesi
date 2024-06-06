using ETicaret.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.Controllers
{
    public class AdminController : Controller
    {
        
        [Authorize(Roles = "Admin")]
        public IActionResult Index(ETicaretNewContext eTicaretNewContext)
        {
            var kategori = eTicaretNewContext.Kategoris.ToList();
            var viewModel = kategori.Select(p => new ProductViewModel
            {
                Id = p.KategoriId,
                Name = p.Adi
              
            }).ToList();
            return View(kategori);
        }

        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        private class ProductViewModel
        {
            public object Id { get; set; }
            public object Name { get; set; }
            public object Price { get; set; }
        }
    }
}
