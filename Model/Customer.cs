

namespace ASPLoanMSC103.Model
{
    public class Customer : BaseEntity
    {
        public int CustomerId { get; set; }
        public decimal? Income { get; set; } = 0;
        public decimal? Expense { get; set; } = 0;
        public string? IdCard { get; set; } = string.Empty;
        public string? PasswortNo { get; set; } = string.Empty;
    }
}