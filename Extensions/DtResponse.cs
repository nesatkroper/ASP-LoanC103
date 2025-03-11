
namespace ASPLoanC103.Extensions
{
    public record struct DtResponse<T>(int Draw, int RecordsTotal, int RecordsFiltered, IEnumerable<T> Data);

}