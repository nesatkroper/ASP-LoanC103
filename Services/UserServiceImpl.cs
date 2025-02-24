
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ASPLoanMSC103.Data;
using ASPLoanMSC103.Model;
using System.Security.Claims;

namespace ASPLoanMSC103.Services
{
    public sealed class UserServiceImpl : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private ILogger<UserServiceImpl> _logger;


        public UserServiceImpl(AppDbContext context, IHttpContextAccessor httpContext, ILogger<UserServiceImpl> logger)
        {
            _context = context;
            _httpContext = httpContext;
            _logger = logger;
        }

        public async Task<bool> LogUserOut()
        {
            _logger.LogInformation("Logging user out");
            try
            {
                var httpContext = _httpContext.HttpContext;
                await httpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging user out");
                return false;
            }
        }

        public async Task<MsgResponse> GetAuthenticatedUser(LoginRequest request)
        {
            var httpContext = _httpContext.HttpContext;
            var user = _context.Users.Where(u => u.UserName == request.UserName).FirstOrDefault();

            if (user is null) return new MsgResponse(false, $"{request.UserName} does not exist.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            { return new MsgResponse(false, "Invalid Password"); }

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user. UserID. ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.RoleName ?? "")
            };

            var claimIdentities = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrinciple = new ClaimsPrincipal(claimIdentities);
            await httpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrinciple, new AuthenticationProperties
            {
                IsPersistent = request.RememberMe,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddDays(1),
            });

            return (new MsgResponse(true, "Login Success"));
        }



        public async Task<MsgResponse> CreateUser(RegisterRequest req)

        {

            var existingUser = _context.Users.Where(x => x.UserName.Contains(req.UserName) ||
            x.Email.Contains(req.Email)).FirstOrDefault();

            if (existingUser is not null)
            {
                return new MsgResponse(false, $"{req.UserName} And/OR {req.Email} already register.");
            }

            var hash = BCrypt.Net.BCrypt.HashPassword(req.Password);
            var user = new User
            {
                Email = req.Email,
                Password = hash,
                UserName = req.UserName,
                RoleName = req.RoleName
            };

            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0 ?
            new MsgResponse(true, "New User Was created.") :
            new MsgResponse(false, "There are something wrong.");
        }
    }
}