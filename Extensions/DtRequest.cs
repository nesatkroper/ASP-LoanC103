



using Newtonsoft.Json;

namespace ASPLoanC103.Extensions
{
    public class DtRequest
    {
        [JsonProperty("draw")]
        public int Draw { get; set; }

        [JsonProperty("draw")]
        public List<Column>? Columns { get; set; }

        [JsonProperty("order")]
        public Order[]? Order { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("search")]
        public Search? Search { get; set; }
    }
}




