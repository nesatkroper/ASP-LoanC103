namespace ASPLoanMSC103.Model
{
    public class User
    {
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}