

namespace ASPLoanC103.Extensions
{
    public sealed class Column
    {
        public string? Data { get; set; }
    }

    public sealed class Order
    {
        public int Column { get; set; }
        public string? Dir { get; set; }
    }

    public sealed class Search
    {
        public string? Value { get; set; }
    }
}