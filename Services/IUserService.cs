
using ASPLoanMSC103.Model;

namespace ASPLoanMSC103.Services
{
    public interface IUserService
    {
        Task<MsgResponse> CreateUser(RegisterRequest req);
        Task<MsgResponse> GetAuthenticatedUser(LoginRequest req);
        Task<bool> LogUserOut();
    }
}