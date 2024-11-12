
using System.Text.Json.Serialization;

namespace Population.Domain.Models
{
    public class PopulationDataFromJsonEntity
    {
        [JsonPropertyName("error")]
        public bool Error { get; set; }
        [JsonPropertyName("msg")]
        public string Msg { get; set; }
        [JsonPropertyName("data")]
        public List<CountryInfoFromJsonEntity> CountryInfo { get; set; }
    }
}


