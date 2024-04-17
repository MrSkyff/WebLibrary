using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.Account.Data.Interfaces;
using WebProject.Areas.Account.Data.Repositories;
using WebProject.Areas.Account.Data.Services;
using WebProject.Models.Account;
using WebProject.Areas.Library.Data.Interfaces;
using WebProject.Areas.Library.Data.Repositories;
using WebProject.Data;
using WebProject.Data.Interfaces;
using WebProject.Data.Repository;
using WebProject.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Setup connection to the SQLServer
//IConfigurationRoot connection = new ConfigurationBuilder()
//    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
//    .AddJsonFile("appsettings.json").Build();

var dbConntetion = Environment.GetEnvironmentVariable("DB_CONNECTION");

builder.Services.AddDbContext<ProjectDbContext>(options => 
    options.UseSqlServer(dbConntetion));


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<ProjectDbContext>()
  .AddDefaultTokenProviders()
  .AddClaimsPrincipalFactory<CustomClaimService>();

// Some paths for user action depend of status.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "WebWiser";
    options.LoginPath = "/Account/Identity/Login";
    options.AccessDeniedPath = "/AccessDenied";
    options.LogoutPath = "/Account/Identity/Login";
    options.ExpireTimeSpan = TimeSpan.FromDays(int.Parse(Environment.GetEnvironmentVariable("LOGOUT_COOKIES_IN_DAYS")!));
    options.SlidingExpiration = true;
});

builder.Services.AddTransient<IUserShared, UserSharedRepository>();
builder.Services.AddTransient<IInvite, InviteRepository>();

builder.Services.AddTransient<ILibraryManager, LibraryManagerRepository>();
builder.Services.AddTransient<ICategoryManager, CategoryManagerRepository>();
builder.Services.AddTransient<ILibrary, LibraryRepository>();
builder.Services.AddTransient<ICategory, CategoryRepository>();
builder.Services.AddTransient<IArticle, ArticleRepository>();
builder.Services.AddTransient<IArticleManager, ArticleManagerRespository>();
builder.Services.AddTransient<ITag, TagRepository>();
builder.Services.AddTransient<ITagManager, TagManagerRepository>();
builder.Services.AddTransient<ISearch, SearchRepository>();

if (false)
{
    builder.Services.AddTransient<IEmailService, EmailServiceLocalTest>();
}
else
{
    builder.Services.AddTransient<IEmailService, EmailService>();
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Account",
    pattern: "{area:exists=Account}/{controller=Identity}/{action=Login}/{value?}");

app.MapControllerRoute(
    name: "Library",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{value?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{value?}");

app.Run();
