

using System.ComponentModel.DataAnnotations.Schema;

namespace ASPLoanMSC103.Model
{
    public class LoanPaymentLines
    {
        public int Id { get; set; }
        public int LoanPaymentId { get; set; }
        public PaymentType? PaymentType { get; set; }
        public decimal Amount { get; set; } = 0;
        public string? Description { get; set; }

        [ForeignKey("LoanPaymentId")]
        public LoanPayment? LoanPayment { get; set; }
    }
}