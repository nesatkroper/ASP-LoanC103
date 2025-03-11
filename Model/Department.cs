

namespace ASPLoanMSC103.Model
{
    public sealed class Department
    {
        public int ID { get; set; }
        public required string DepartmentName { get; set; }
        public string? Description { get; set; }
    }
}