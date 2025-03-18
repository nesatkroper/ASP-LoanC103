
using System.ComponentModel.DataAnnotations.Schema;


namespace ASPLoanC103.Model
{
    public sealed class LoanSchedule
    {
        public long Id { get; set; }
        [ForeignKey(nameof(Loan))]
        public long LoanId { get; set; }
        public int LineSeq { get; set; } = 1;
        public int Days { get; set; } = 0;
        public DateTime? PaymentDate { get; set; }
        public decimal PaidPrinciple { get; set; } = 0;
        public decimal InterestAmount { get; set; } = 0;
        public decimal TotalPayment { get; set; } = 0;
        public decimal OutStanding { get; set; } = 0;
        public bool IsPaid { get; set; } = false;
    }
}


