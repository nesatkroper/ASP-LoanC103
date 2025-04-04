

using ASPLoanMSC103.Extensions;
using ASPLoanMSC103.Model;
using ASPLoanMSC103.Model.ViewModels;

namespace ASPLoanMSC103.Services
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