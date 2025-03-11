

namespace ASLoanC103.Model.ViewModels
{
    public class LoanViewModel
    {
        public long LoanId { get; set; }

        public int CustomerId { get; set; }

        public string? LoanCategory { get; set; }

        public DateTime? RegisterDate { get; set; }

        public int? Installment { get; set; }

        public decimal? Principle { get; set; }

        public decimal? InterestRate { get; set; }

        public string? PaymentMethod { get; set; }

        public bool IsActive { get; set; } = true;

        public string? Description { get; set; }
    }
}

