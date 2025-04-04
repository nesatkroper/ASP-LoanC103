using ASPLoanC103.Services;
using ASPLoanMSC103.Data;
using ASPLoanMSC103.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

// builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores

builder.Services.AddScoped<ILoanServices, LoanServiceImpl>();
builder.Services.AddScoped<IPaymentService, PaymentServiceImpl>();


var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
