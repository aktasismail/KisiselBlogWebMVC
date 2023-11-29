using ismailaktasblog.DataAccess;
using ismailaktasblog.DataAccess.Abstract;
using ismailaktasblog.DataAccess.Concrete;
using ismailaktasblog.Entities;
using ismailaktasblog.Models;
using ismailaktasblog.Services.Abstract;
using ismailaktasblog.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ApplicationDbContext>(option
    => option.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]));
builder.Services.AddIdentity<AppUser,IdentityRole>(option =>
{
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequiredLength = 8;
    option.Password.RequiredUniqueChars = 1;
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    option.Lockout.MaxFailedAccessAttempts = 100;
}).AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); ;
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/GirisYap";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});
builder.Services.AddScoped<IMakaleDal, MakaleDal>();
builder.Services.AddScoped<IMakaleService, MakaleService>();
builder.Services.AddScoped<IKategoriDal, KategoriDal>();
builder.Services.AddScoped<IKategoriService, KategoriService>();
builder.Services.AddScoped<IMesajDal, MesajDal>();
builder.Services.AddScoped<IMesajService, MesajService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IMailSender, EmailSender>(i =>
    new EmailSender(
        builder.Configuration["EmailSender:Host"],
        builder.Configuration.GetValue<int>("EmailSender:Port"),
        builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
        builder.Configuration["EmailSender:Username"],
        builder.Configuration["EmailSender:Password"])
);
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
