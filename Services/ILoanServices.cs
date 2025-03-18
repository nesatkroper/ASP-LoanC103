

using ASLoanC103.Model.ViewModels;
using ASPLoanC103.Extensions;
using ASPLoanC103.Model;
using ASPLoanMSC103.Model;

namespace ASPLoanC103.Services
{
    public interface ILoanServices
    {
        void CreateLoan(Loan req);
        Task<Loan?> GetLoanById(long loanId);
        Task<Loan?> GetLoanWithSchedules(long loanId);
        Task<DtResponse<LoanViewModel>> Gets(DtRequest request);
        Task<Payment?> GetSchedulePaymentById(long loanId, int scheduleId);
    }
}