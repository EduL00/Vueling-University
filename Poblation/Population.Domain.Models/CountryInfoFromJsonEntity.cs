
using System.Text.Json.Serialization;

namespace Population.Domain.Models
{
    public class CountryInfoFromJsonEntity
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("iso3")]
        public string Iso3 { get; set; }
        [JsonPropertyName("populationCounts")]
        public List<PopulationCountryInfoFromJsonEntity> PopulationCountryInfo { get; set; }

        public bool NameStartWithCharacter(string character)
        {
            return (Country[0] == character[0]);
        }
    }

}
