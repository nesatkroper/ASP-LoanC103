

using Microsoft.EntityFrameworkCore;

namespace ASPLoanMSC103.Configurations
{
    public interface IBaseEntityTypeConfigurations<T> : IEntityTypeConfiguration<T> where T : class;
}