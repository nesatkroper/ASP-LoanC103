using ASPLoanMSC103.Data;
using ASPLoanMSC103.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DapperFactory>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserService, UserServiceImpl>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
  options.LogoutPath = new PathString("/Account/Login");
  options.AccessDeniedPath = new PathString("/Account/AccessDenied");
  options.ReturnUrlParameter = "ReturnUrl";
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
