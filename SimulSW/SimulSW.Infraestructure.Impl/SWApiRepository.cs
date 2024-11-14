using SimulSW.Infraestructure.Contracts;
using SimulSW.Infraestructure.Contracts.APIEntities;
using System.Text.Json;

namespace SimulSW.Infraestructure.Impl
{
    public class SWApiRepository : ISWApiRepository
    {
        public async Task<APIInfoFromJsonEntity> GetApiInfo()
        {

            using HttpClient client = new HttpClient();
            HttpResponseMessage data = await client.GetAsync("https://swapi.dev/api/planets/?format=json");
            string dataAsString = await data.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<APIInfoFromJsonEntity>(dataAsString);
        }

        public async Task<List<string>> GetPopulationNames(string planetUrlInfo)
        {
            List<string> populationNames = new();

            using HttpClient client = new HttpClient();
            HttpResponseMessage data = await client.GetAsync(planetUrlInfo);
            string dataAsString = await data.Content.ReadAsStringAsync();

            PlanetInfoFromJsonEntity planetInfo = JsonSerializer.Deserialize< PlanetInfoFromJsonEntity> (dataAsString);

            foreach (string residentInfo in planetInfo.Residents)
            {
                using HttpClient residentClient = new HttpClient();
                HttpResponseMessage residentData = await residentClient.GetAsync(residentInfo);
                string residentDataAsString = await residentData.Content.ReadAsStringAsync();

                ResidentInfoFromJsonEntity resident = JsonSerializer.Deserialize<ResidentInfoFromJsonEntity>(residentDataAsString);

                populationNames.Add(resident.Name);
            }


            return populationNames;
        }

    }
}
