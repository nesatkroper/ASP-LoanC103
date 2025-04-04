

namespace ASPLoanMSC103.Extensions
{
    public class LoanCalculator
    {
        private decimal? _principle;
        private decimal? _interestRate;
        private int? _installment;

        public LoanCalculator(decimal? principle, decimal? interestRate, int? installment)

        {
            _principle = principle;
            _interestRate = interestRate;
            _installment = installment;
        }

        public decimal? Principle { get => _principle; }
        public decimal? InterestRate { get => _interestRate; }
        public int? Installment { get => _installment; }
    }
}