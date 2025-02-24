using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPLoanMSC103.Model
{
    public class Department
    {
        public int ID { get; set; }
        public required string DepartmentName { get; set; }
        public string? Description { get; set; }
    }
}