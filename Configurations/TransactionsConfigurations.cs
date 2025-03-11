
using ASPLoanC103.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASPLoanC103.Configurations
{
    public class TransactionsConfigurations : IBaseEntityTypeConfigurations<Transactions>
    {
        public void Configure(EntityTypeBuilder<Transactions> builder)
        {
            builder.ToTable("transactions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TransactionDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.Created).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.Updated).HasDefaultValueSql("GETDATE()");
            builder.HasOne(x => x.Loan).WithOne().HasForeignKey<Loan>(x => x.TransactionId);
        }
    }
}