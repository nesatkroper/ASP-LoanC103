
using System.ComponentModel.DataAnnotations.Schema;


namespace ASPLoanMSC103.Model
{
    public sealed class Employee : BaseEntity
    {
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
        public decimal? Salary { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public DateTime? HireDate { get; set; }
        public string? Position { get; set; }
        public Department? Department { get; set; }
    }
}