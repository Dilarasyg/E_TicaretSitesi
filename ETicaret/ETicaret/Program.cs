using ETicaret.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ETicaretNewContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

void ConfigureServices(IServiceCollection services)
{
    services.AddSession();// Oturum y�netimini etkinle�tirme
}

void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
{
    app.UseSession();// Oturum y�neticisini etkinle�tirme
}

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
 {
     x.Cookie.Name = "LoginCookie";//�erez tabanl� kimlik do�rulama kullan�l�rken olu�turulan �erezin ad�n� belirler.
     x.LoginPath = "/UserLogin/Login";//oturum a��lmad��� durumda y�nlendirilece�i giri� sayfas�n�n url'si
     x.LogoutPath = "/UserLogin/Logout";//��k�� url'si    
     x.ExpireTimeSpan = TimeSpan.FromMinutes(2);//oturumun ne kadar s�re boyunca kullan�c� taraf�ndan eri�ebilir oldu�unu belirler.
     //TimeSpan.FromMinutes(2) ifadesi 2 dakika boyunca etkin bir oturumu  olacak.Oturum s�resi, kullan�c�n�n bir etkinlik ger�ekle�tirmemesi durumunda zaman a��m�na u�rayacak ve oturum sonlanacakt�r.


     //LOG�N SAYFALARI KARI�MAMASI ���N EKLEND�
     x.Events = new CookieAuthenticationEvents
     {
         OnRedirectToLogin = context =>
         { 
             //RED�RECT: Bir sayfay� ya da URL'yi ba�ka bir sayfaya ya da URL'ye y�nlendirme i�lemi

             //Redirect yap�lacak controller ve actiom'� belirleme
             if (context.Request.Path.StartsWithSegments("/Admin"))//istek yolunun /Admin ile ba�lay�p ba�lamad���n� kontrol eder.
             {
                 context.Response.Redirect("/Login/Index");// �yle ise admin login'e

             }
             else
             {
                 context.Response.Redirect("/UserLogin/Login");//de�ilse kullan�c� login'e gider
             }
             return Task.CompletedTask; //genellikle bir asenkron i�levin sonunda, i�lev tamamland���n� belirtmek i�in kullan�l�r.
         }
     };
 }); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
