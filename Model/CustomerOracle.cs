

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPLoanMSC103.Model
{
    public class CustomerOracle
    {
        [Key]
        public int CustomerId { get; set; }
        public decimal? Income { get; set; } = 0;
        public decimal? Expense { get; set; } = 0;
        public string? IdCard { get; set; } = string.Empty;
        public string? PasswortNo { get; set; } = string.Empty;

        [Column(TypeName = "NUMBER(1,0)")]
        public bool? IsActive { get; set; } = false;
    }
}