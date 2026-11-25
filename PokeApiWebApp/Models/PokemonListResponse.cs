using System.Text.Json.Serialization;

namespace PokeApiWebApp.Models
{
    /// <summary>
    /// Respuesta de GET /pokemon?offset=0&amp;limit=20 (listado paginado).
    /// </summary>
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
