using System.Text.Json.Serialization;

namespace PokeApiWebApp.Models
{
    //listado
    public class PokemonListResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("next")]
        public string? Next { get; set; }

        [JsonPropertyName("previous")]
        public string? Previous { get; set; }

        [JsonPropertyName("results")]
        public List<NamedApiResource> Results { get; set; } = new();
    }
}
