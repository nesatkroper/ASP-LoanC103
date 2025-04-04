

namespace ASPLoanMSC103.Model
{
    public class LoanPayment
    {
        public int Id { get; set; }
        public Guid? TransactionId { get; set; }
        public Guid? UserId { get; set; }
        public int ScheduleId { get; set; }
        public long LoanId { get; set; }
        public decimal PaidPrinciple { get; set; } = 0;
        public decimal PaidInterest { get; set; } = 0;
        public decimal RecieveAmount { get; set; } = 0;
        public decimal ChangedAmount { get; set; } = 0;
        public virtual ICollection<LoanPaymentLines> LoanPaymentLines { get; set; } = [];
    }
}