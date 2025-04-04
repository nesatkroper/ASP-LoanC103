
using ASPLoanMSC103.Model;

namespace ASPLoanMSC103.Services
{
    public interface IPaymentService
    {
        bool Create(Payment payment);
    }
}