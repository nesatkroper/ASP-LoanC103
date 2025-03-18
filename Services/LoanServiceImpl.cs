

using ASLoanC103.Model.ViewModels;
using ASPLoanC103.Extensions;
using ASPLoanC103.Model;
using ASPLoanMSC103.Data;
using ASPLoanMSC103.Model;
using Microsoft.EntityFrameworkCore;

namespace ASPLoanC103.Services
{
    public class LoanServiceImpl : ILoanServices
    {
        private readonly AppDbContext _context;
        public LoanServiceImpl(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DtResponse<LoanViewModel>> Gets(DtRequest request)
        {
            IQueryable<Loan> query = _context.Loans;

            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                string searchValue = request.Search.Value.ToLower();
                query = query.Where(x => x.Principle.ToString().Contains(searchValue) || x.InterestRate.ToString()!.Contains(searchValue) || x.CustomerId.ToString()!.Contains(searchValue));
            }

            if (request.Order != null && request.Order.Any())
            {
                var columnIndex = request.Order[0].Column;
                var sortDirection = request?.Order[0]?.Dir?.ToLower() == "asc";
                var columnName = request?.Columns?[columnIndex]?.Data;

                var column = typeof(LoanViewModel).GetProperties().FirstOrDefault(x => string.Equals(x.Name, columnName, StringComparison.OrdinalIgnoreCase))?.Name ?? "";

                if (!string.IsNullOrEmpty(column))
                {
                    query = sortDirection ? query.OrderBy(e => EF.Property<object>(e, column)) : query.OrderByDescending(e => EF.Property<object>(e, column));
                }
            }

            int totalRecords = await query.CountAsync();
            var data = await query.Skip(request!.Start).Take(request.Length).Select(x => new LoanViewModel
            {
                LoanId = x.LoanId,
                CustomerId = x.CustomerId,
                Principle = x.Principle,
                PaymentMethod = x.PaymentMethod.ToString(),
                InterestRate = x.InterestRate,
                RegisterDate = x.RegsiterDate,
                Description = x.Description,
            }).ToListAsync();


            var response = new DtResponse<LoanViewModel>
            {
                Draw = request.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = totalRecords,
                Data = data
            };
            return response;
        }

        public void CreateLoan(Loan req)
        {
            try
            {
                var transactions = new Transactions { Description = req.Description, TransactionTypes = TransactionTypes.Disbursement };

                var loanSchedules = new LoanCalculator(req.Principle, req.InterestRate, req.Installment);

                var schedules = LoanCalculatorExtensions.CreateSchedule(loanSchedules);
                req.LoanSchedules.AddRange(schedules);
                transactions.Loan = req;
                _context.Transactions.Add(transactions);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<Loan?> GetLoanWithSchedules(long loanId)
        {
            var loanSchedule = await _context.Loans.Include(l => l.LoanCategory).Include(s => s.LoanSchedules).Where(x => x.LoanId == loanId).FirstOrDefaultAsync();

            return loanSchedule;
        }

        public async Task<Loan?> GetLoanById(long loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            return loan;
        }

        public async Task<Payment?> GetSchedulePaymentById(long loanId, int scheduleId)
        {
            var paymentSchedule = await _context.LoanSchedules.Where(x => x.LoanId == loanId && x.Id == scheduleId && x.IsPaid == false).Select(s => new Payment
            {
                PaidPrinciple = s.PaidPrinciple,
                PaidInterest = s.InterestAmount,
                ChangeAmount = 0,
                RecieveAmount = s.TotalPayment
            }).FirstOrDefaultAsync();
            return paymentSchedule;
        }
    }
}