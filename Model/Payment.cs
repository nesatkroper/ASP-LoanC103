

namespace ASPLoanMSC103.Model
{
    public sealed class Payment
    {
        public string UserId { get; set; } = string.Empty;
        public long LoanId { get; set; }
        public int ScheduleId { get; set; }
        public int PaymentId { get; set; }
        public decimal? PaidPrinciple { get; set; }
        public decimal? PaidInterest { get; set; }
        public decimal? RecieveAmount { get; set; }
        public decimal? ChangeAmount { get; set; }
        public decimal? GetTotal => PaidInterest + PaidPrinciple;
    }
}