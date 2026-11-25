using System.Text.Json.Serialization;

namespace PokeApiWebApp.Models
{
    /// API
    /// "https://pokeapi.co/api/v2/pokemon/1/" }
    public class NamedApiResource
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;


        /// Extrae el número de ID del final de la URL
        [JsonIgnore]
        public int Id
        {
            get
            {
                var ultimoSegmento = Url.TrimEnd('/').Split('/').LastOrDefault();
                return int.TryParse(ultimoSegmento, out var id) ? id : 0;
            }
        }
    }
}
