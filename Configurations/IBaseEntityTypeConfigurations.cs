

using Microsoft.EntityFrameworkCore;

namespace ASPLoanC103.Configurations
{
    public interface IBaseEntityTypeConfigurations<T> : IEntityTypeConfiguration<T> where T : class;
}