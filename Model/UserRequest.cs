

namespace ASPLoanMSC103.Model
{
    public sealed class UserReqpuest
    {

        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public string? Role { get; set; }
    }

}