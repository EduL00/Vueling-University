using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Universities.Infraestructure.Contracts.JSONEntities
{
    public class JSONListUniversityEntities
    {
        [JsonPropertyName("Property1")]
        public List<JSONUniversityEntity> Universities { get; set; }
    }
}
