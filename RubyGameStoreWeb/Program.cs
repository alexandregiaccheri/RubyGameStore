using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Helper;
using Stripe;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<RubyGameStoreDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddDefaultTokenProviders().AddEntityFrameworkStores<RubyGameStoreDbContext>();
builder.Services.Configure<StripeKeys>(builder.Configuration.GetSection("StripeKeys"));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = builder.Configuration.GetValue<string>("FacebookLogin:AppId");
        options.AppSecret = builder.Configuration.GetValue<string>("FacebookLogin:AppSecret");
    }).AddTwitter(options =>
    {
        options.ConsumerKey = builder.Configuration.GetValue<string>("TwitterLogin:ClientId");
        options.ConsumerSecret = builder.Configuration.GetValue<string>("TwitterLogin:ClientSecret");
    }).AddGoogle(options =>
    {
        options.ClientId = builder.Configuration.GetValue<string>("GoogleLogin:ClientId");
        options.ClientSecret = builder.Configuration.GetValue<string>("GoogleLogin:ClientSecret");
    });
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = @"/Identity/Account/Login";
    options.LogoutPath = @"/Identity/Account/Logout";
    options.AccessDeniedPath = @"/Identity/Account/AccessDenied";
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

StripeConfiguration.ApiKey = builder.Configuration.GetSection("StripeKeys:SecretKey").Get<string>();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");

app.Run();
