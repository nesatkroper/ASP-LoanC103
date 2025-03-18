

using ASPLoanC103.Model;
using ASPLoanMSC103.Model;

namespace ASPLoanC103.Extensions
{
    public class LoanCalculatorExtensions
    {
        public static IEnumerable<LoanSchedule> CreateSchedule(LoanCalculator schedule, PaymentMethod paymentMethod = PaymentMethod.Declined)
        {
            return paymentMethod switch
            {
                PaymentMethod.Declined => CalDecline(schedule.Installment, schedule.InterestRate, schedule.Principle),
                _ => CalDecline(schedule.Installment, schedule.InterestRate, schedule.Principle),
            };
        }

        private static decimal Interest(decimal? amount = 0, decimal? rate = 0)
        {
            decimal _amount = amount ?? 0;
            decimal _rate = rate ?? 0;
            return Math.Round(_amount * (_rate / 100), 2);
        }

        private static decimal TotalPay(decimal? amount = 0, decimal? interestAmount = 0)
        {
            decimal _amount = amount ?? 0;
            decimal _interestAmount = interestAmount ?? 0;
            return Math.Round(_amount + _interestAmount, 2);
        }

        private static decimal LastBalance(decimal? amount = 0, decimal? Paidprinciple = 0)
        {
            decimal _amount = amount ?? 0;
            decimal _principle = Paidprinciple ?? 0;
            return Math.Round(_amount - _principle, 2);
        }

        private static IEnumerable<LoanSchedule> CalDecline(int? periodInMonth, decimal? monthlyRate, decimal? amount)
        {
            List<LoanSchedule> loanScheduleLines = [];
            int period = periodInMonth ?? 0;
            decimal _amount = amount ?? 0.00M;
            decimal rate = monthlyRate ?? 0;

            var principalPaid = Math.Round(_amount / period, 2);
            decimal beginBalance = 0, lastBalance = 0;
            beginBalance = lastBalance = _amount;

            for (int i = 1; i <= period; i++)
            {
                DateTime paymentDate = DateTime.Now;
                var totalPay = 0.00M;
                var interest = 0.00M;
                beginBalance = lastBalance;
                interest = Interest(beginBalance, rate);
                totalPay = TotalPay(principalPaid, interest);
                paymentDate = paymentDate.AddMonths(i);

                if (i < period)
                {
                    lastBalance = LastBalance(beginBalance, principalPaid);
                }
                else
                {
                    principalPaid = beginBalance;
                    lastBalance = 0;
                }
                LoanSchedule line = new LoanSchedule
                {
                    LineSeq = i,
                    PaymentDate = paymentDate,
                    PaidPrinciple = principalPaid,
                    InterestAmount = interest,
                    TotalPayment = totalPay,
                    OutStanding = lastBalance
                };
                loanScheduleLines.Add(line);
            }
            return loanScheduleLines;
        }
    }
}

