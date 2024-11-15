using System.Text.Json;
using Universities.Infraestructure.Contracts;
using Universities.Infraestructure.Contracts.DBEntities;
using Universities.Infraestructure.Contracts.JSONEntities;

namespace Universities.Infraestructure.Impl
{
    public class APIRepository : IAPIRepository
    {
        public async Task<JSONListUniversityEntities> GetApiInfo()
        {
            JSONListUniversityEntities dataList = new JSONListUniversityEntities()
            {
                Universities = new()
            };

            HttpClient client = new HttpClient();
            HttpResponseMessage data = await client.GetAsync("http://universities.hipolabs.com/search");
            string dataAsString = await data.Content.ReadAsStringAsync();

            var info = JsonSerializer.Deserialize<List<JSONUniversityEntity>>(dataAsString);

            foreach (JSONUniversityEntity univ in info)
            {
                dataList.Universities.Add (univ);
            }

            return dataList;
        }
    }
}
