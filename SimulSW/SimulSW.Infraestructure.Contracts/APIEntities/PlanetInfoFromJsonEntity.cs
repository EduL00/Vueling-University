using System.Text.Json.Serialization;

namespace SimulSW.Infraestructure.Contracts.APIEntities
{
    public class PlanetInfoFromJsonEntity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("rotation_period")]
        public string Rot { get; set; }
        [JsonPropertyName("orbital_period")]
        public string Perd { get; set; }
        [JsonPropertyName("climate")]
        public string Climate { get; set; }
        [JsonPropertyName("population")]
        public string Population { get; set; }
        [JsonPropertyName("residents")]
        public List<string> Residents { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
