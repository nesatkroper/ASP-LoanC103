

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ASPLoanMSC103.Model;

namespace ASPLoanC103.Model
{
    public enum PaymentMethod
    {
        Declined = 1,
        Flat,
        Balloon,
        Annurity
    }

    public class Loan
    {
        public long LoanId { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? TransactionId { get; set; }

        public int CustomerId { get; set; }

        public int EmployeeId { get; set; }

        public int LoanCategoryId { get; set; }

        public DateTime? RegsiterDate { get; set; } = DateTime.Now;

        public int Installment { get; set; } = 12;

        public decimal Principle { get; set; } = 0;

        public decimal InterestRate { get; set; }

        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Declined;

        public bool IsActive { get; set; } = true;

        public string? Description { get; set; }

        [ForeignKey(nameof(LoanCategoryId))]
        public virtual LoanCategory? LoanCategory { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee? Employee { get; set; }

        public virtual List<LoanSchedule> LoanSchedules { get; set; } = [];
    }
}
