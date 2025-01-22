namespace ASPLoanMSC103.Model
{
  public class LoanCategory
  {
    public int LoanCategoryID { get; set; }
    public string? LoanCategoryCode { get; set; }
    public string? Description { get; set; }
    public string? DesInKhmer { get; set; }
    public string? Code { get; set; }
    public DateTime? CreatedDT { get; set; }
    public DateTime? ModifiedDT { get; set; }
    public int EditSeq { get; set; }
    public bool IsActive { get; set; }
  }
}
