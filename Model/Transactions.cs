

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPLoanMSC103.Model
{
    public enum TransactionTypes
    {
        Disbursement = 1,
        RecievePayment = 2,
        Penalty = 3,
    }

    public sealed class Transactions
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public TransactionTypes? TransactionTypes { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? TransactionDate { get; set; }
        public DateTimeOffset? Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public Loan? Loan { get; set; }
        // public loanpa MyProperty { get; set; }
    }
}









