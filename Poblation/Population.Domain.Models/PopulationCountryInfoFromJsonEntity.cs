
using System.Text.Json.Serialization;

namespace Population.Domain.Models
{
    public class PopulationCountryInfoFromJsonEntity
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }
        [JsonPropertyName("value")]
        public long Value { get; set; }
    }
}
