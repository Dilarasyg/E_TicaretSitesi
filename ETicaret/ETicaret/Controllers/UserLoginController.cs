using ETicaret.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaret.Controllers
{
    public class UserLoginController : Controller
    {
       
            private readonly ETicaretNewContext _context;
            public UserLoginController()
            {
                _context = new ETicaretNewContext();
            }

            [HttpGet]
            public IActionResult Register()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Register([FromForm] Uye user)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Login));
                }
                return View(user); 
            }
            public IActionResult Login()
            {
                return View();
            }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] Uye entity)
        {
            try
            {
                if (ModelState.IsValid)//viewden gelen modelde bir sıkıntı var mı
                {
                    if (CheckUser(entity.Email, entity.Sifre))//user var mı yok mu diye kontrol ettik
                    {
                        var uye = _context.Uyes.FirstOrDefault(x => x.Email == entity.Email && x.Sifre == entity.Sifre);
                        var sepetid = 0;
                        if (uye != null)
                        {
                            var sepet = _context.Siparisses.FirstOrDefault(x => x.UyeId == uye.UyeId && x.ÖdemeDurumu == "Onaysız");//kullanıcının sepet durumunu kontrol ettik
                            if (sepet != null)
                            {
                                sepetid = sepet.SiparisId;
                            }
                            else
                            {
                                Sipariss yeniSepet = new Sipariss
                                {
                                    ÖdemeDurumu = "Onaysız",
                                    Uye = uye,
                                    UyeId = uye.UyeId,
                                    ToplamTutar = 0
                                };
                                _context.Siparisses.Add(yeniSepet);
                                _context.SaveChanges();
                                sepetid = yeniSepet.SiparisId;

                            }
                        }
                        var claims = new List<Claim>();
                        claims.Add(new Claim
                            (ClaimTypes.NameIdentifier, entity.Email));
                        claims.Add(new Claim
                            (ClaimTypes.Sid, sepetid.ToString()));//yeni claim nesnesinde kullanıcının kimlik bilgilerinden emailini tanımlamak içindir.
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);
                        return RedirectToAction("Index", "Defult");

                    }
                    else
                    {
                        throw new Exception("User not found!");
                    }
                }
                else
                {
                    throw new Exception("Please check form data!");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(entity);
        }
        private bool CheckUser(string email, string sifre)
        {
            var user = _context.Uyes.FirstOrDefault(x => x.Email == email && x.Sifre == sifre);

            return user != null;
        }
        //public IActionResult Logout()
        //{
        //	return View();
        //}
        public async Task<IActionResult> LogOut()
        {
            //kullanıcıyı belirtilen kimlik doğrulama şemasından çıkış yapmak için kullanılır
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "UserLogin");

        }

    }
    }

