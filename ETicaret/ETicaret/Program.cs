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
    services.AddSession();// Oturum yönetimini etkinleþtirme
}

void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
{
    app.UseSession();// Oturum yöneticisini etkinleþtirme
}

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
 {
     x.Cookie.Name = "LoginCookie";//Çerez tabanlý kimlik doðrulama kullanýlýrken oluþturulan çerezin adýný belirler.
     x.LoginPath = "/UserLogin/Login";//oturum açýlmadýðý durumda yönlendirileceði giriþ sayfasýnýn url'si
     x.LogoutPath = "/UserLogin/Logout";//çýkýþ url'si    
     x.ExpireTimeSpan = TimeSpan.FromMinutes(2);//oturumun ne kadar süre boyunca kullanýcý tarafýndan eriþebilir olduðunu belirler.
     //TimeSpan.FromMinutes(2) ifadesi 2 dakika boyunca etkin bir oturumu  olacak.Oturum süresi, kullanýcýnýn bir etkinlik gerçekleþtirmemesi durumunda zaman aþýmýna uðrayacak ve oturum sonlanacaktýr.


     //LOGÝN SAYFALARI KARIÞMAMASI ÝÇÝN EKLENDÝ
     x.Events = new CookieAuthenticationEvents
     {
         OnRedirectToLogin = context =>
         { 
             //REDÝRECT: Bir sayfayý ya da URL'yi baþka bir sayfaya ya da URL'ye yönlendirme iþlemi

             //Redirect yapýlacak controller ve actiom'ý belirleme
             if (context.Request.Path.StartsWithSegments("/Admin"))//istek yolunun /Admin ile baþlayýp baþlamadýðýný kontrol eder.
             {
                 context.Response.Redirect("/Login/Index");// öyle ise admin login'e

             }
             else
             {
                 context.Response.Redirect("/UserLogin/Login");//deðilse kullanýcý login'e gider
             }
             return Task.CompletedTask; //genellikle bir asenkron iþlevin sonunda, iþlev tamamlandýðýný belirtmek için kullanýlýr.
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
