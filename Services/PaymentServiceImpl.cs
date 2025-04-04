using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPLoanC103.Model;
using ASPLoanMSC103.Data;
using ASPLoanMSC103.Model;

namespace ASPLoanMSC103.Services
{
    public class PaymentServiceImpl : IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentServiceImpl(AppDbContext context)
        {
            _context = context;
        }

        public bool Create(Payment payment)
        {
            try
            {
                var existingLoan = _context.Loans.Where(l => l.LoanId == payment.LoanId).ToList();

                if (!existingLoan.Any()) return false;

                var loanPayment = new LoanPayment
                {
                    UserId = Guid.Parse(payment.UserId),
                    LoanId = payment.LoanId,
                    ScheduleId = payment.ScheduleId,
                    PaidInterest = payment.PaidInterest!.Value,
                    PaidPrinciple = payment.PaidPrinciple!.Value,
                    RecieveAmount = payment.RecieveAmount!.Value,
                    ChangedAmount = payment.ChangeAmount!.Value
                };

                var transactions = new Transactions
                {
                    TransactionDate = DateTime.Now,
                    TransactionTypes = TransactionTypes.RecievePayment,
                    Description = "Loan Recieve Payment",
                };

                var principle = new LoanPaymentLines
                {
                    Amount = payment.PaidPrinciple!.Value,
                    Description = "Loan Principle Payment",
                    PaymentType = PaymentType.Principle,
                };

                var interest = new LoanPaymentLines
                {
                    Amount = payment.PaidInterest!.Value,
                    Description = "Loan Interest Payment",
                    PaymentType = PaymentType.Interest,
                };

                loanPayment.LoanPaymentLines.Add(principle);
                loanPayment.LoanPaymentLines.Add(interest);
                // transactions.loan = loanPayment;
                _context.Transactions.Add(transactions);
                _context.SaveChanges();

                var updatePaymentSchedule = _context.LoanSchedules.Where(x => x.IsPaid == false && x.LoanId == payment.LoanId && x.Id == payment.ScheduleId).FirstOrDefault();

                updatePaymentSchedule!.IsPaid = true;

                _context.Entry(updatePaymentSchedule).Property(x => x.IsPaid).IsModified = true;

                _context.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
    }
}