using System.Text.Json.Serialization;

namespace SimulSW.Infraestructure.Contracts.APIEntities
{
    public class APIInfoFromJsonEntity
    {
        [JsonPropertyName("results")]
        public List<PlanetInfoFromJsonEntity> PlanetsInfo { get; set; }
    }
}
