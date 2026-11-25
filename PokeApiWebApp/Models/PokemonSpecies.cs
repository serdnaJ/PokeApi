using System.Text.Json.Serialization;

namespace PokeApiWebApp.Models
{
    /// <summary>
    /// Respuesta de GET /pokemon-species/{id}. Se usa para obtener
    /// la descripción de la Pokédex en español.
    /// </summary>
    public class PokemonSpecies
    {
        [JsonPropertyName("flavor_text_entries")]
        public List<FlavorTextEntry> FlavorTextEntries { get; set; } = new();

        [JsonPropertyName("genera")]
        public List<Genus> Genera { get; set; } = new();
    }

    public class FlavorTextEntry
    {
        [JsonPropertyName("flavor_text")]
        public string FlavorText { get; set; } = string.Empty;

        [JsonPropertyName("language")]
        public NamedApiResource Language { get; set; } = new();
    }

    public class Genus
    {
        [JsonPropertyName("genus")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("language")]
        public NamedApiResource Language { get; set; } = new();
    }
}
