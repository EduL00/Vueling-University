using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Universities.Infraestructure.Contracts.JSONEntities
{
    public class JSONUniversityEntity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("alpha_two_code")]
        public string Alpha { get; set; }
        [JsonPropertyName("domains")]
        public List<string> Domains { get; set; }
        [JsonPropertyName("stateprovince")]
        public string Province { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("web_pages")]
        public List<string> Webs { get; set; }
    }

}
