using System.Text.Json.Serialization;

namespace PokeApiWebApp.Models
{
    /// <summary>
    /// Referencia genérica que la PokéAPI usa en muchas respuestas:
    /// { "name": "bulbasaur", "url": "https://pokeapi.co/api/v2/pokemon/1/" }
    /// </summary>
    public class NamedApiResource
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Extrae el número de ID del final de la URL ("/api/v2/pokemon/25/" → 25).
        /// Así evitamos hacer una petición extra por cada Pokémon del listado.
        /// </summary>
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
