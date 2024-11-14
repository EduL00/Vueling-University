using System.Text.Json.Serialization;

namespace SimulSW.Infraestructure.Contracts.APIEntities
{
    public class ResidentInfoFromJsonEntity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
