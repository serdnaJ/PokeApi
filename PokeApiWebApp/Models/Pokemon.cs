using System.Text.Json.Serialization;

namespace PokeApiWebApp.Models
{
    //mapear los campos
    public class Pokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// Altura
        [JsonPropertyName("height")]
        public int Height { get; set; }

        /// Peso
        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("base_experience")]
        public int? BaseExperience { get; set; }

        [JsonPropertyName("sprites")]
        public PokemonSprites Sprites { get; set; } = new();

        [JsonPropertyName("types")]
        public List<PokemonType> Types { get; set; } = new();

        [JsonPropertyName("abilities")]
        public List<PokemonAbility> Abilities { get; set; } = new();

        [JsonPropertyName("stats")]
        public List<PokemonStat> Stats { get; set; } = new();
    }

    public class PokemonSprites
    {
        [JsonPropertyName("front_default")]
        public string? FrontDefault { get; set; }

        [JsonPropertyName("front_shiny")]
        public string? FrontShiny { get; set; }

        [JsonPropertyName("other")]
        public OtherSprites? Other { get; set; }
    }

    public class OtherSprites
    {
        [JsonPropertyName("official-artwork")]
        public OfficialArtwork? OfficialArtwork { get; set; }
    }

    public class OfficialArtwork
    {
        [JsonPropertyName("front_default")]
        public string? FrontDefault { get; set; }
    }

    public class PokemonType
    {
        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        [JsonPropertyName("type")]
        public NamedApiResource Type { get; set; } = new();
    }

    public class PokemonAbility
    {
        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("ability")]
        public NamedApiResource Ability { get; set; } = new();
    }

    public class PokemonStat
    {
        [JsonPropertyName("base_stat")]
        public int BaseStat { get; set; }

        [JsonPropertyName("stat")]
        public NamedApiResource Stat { get; set; } = new();
    }
}
