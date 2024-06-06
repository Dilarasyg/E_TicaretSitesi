using ETicaret.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaret.Controllers
{
    public class LoginController : Controller
    {
        private readonly ETicaretNewContext _context;
        public LoginController()
        {
             _context = new ETicaretNewContext();    
        }
        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if(claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Admin");   
            }
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] Yonetici p)
        {
            var bilgiler = _context.Yoneticis.FirstOrDefault(x => x.KullaniciAdi == p.KullaniciAdi && x.Password == p.Password);
            if (bilgiler != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,p.KullaniciAdi),
                    new Claim("OtherProperties","Dilara"),
                    new Claim(ClaimTypes.Role,"Admin")
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = Convert.ToBoolean(p.Durum)

                };
                 await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                TempData["hata"] = "Giriş bilgileriniz yanlış";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
