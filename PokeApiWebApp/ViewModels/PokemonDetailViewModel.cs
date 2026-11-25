using PokeApiWebApp.Models;

namespace PokeApiWebApp.ViewModels
{
    /// <summary>
    /// Combina los datos de /pokemon/{id} y /pokemon-species/{id}
    /// ya listos para mostrarse en la vista de detalle.
    /// </summary>
    public class PokemonDetailViewModel
    {
        public Pokemon Pokemon { get; set; } = new();
        public string? Description { get; set; }
        public string? Genus { get; set; }

        public string ImageUrl =>
            Pokemon.Sprites.Other?.OfficialArtwork?.FrontDefault
            ?? Pokemon.Sprites.FrontDefault
            ?? string.Empty;

        /// <summary>Altura convertida de decímetros a metros.</summary>
        public double HeightInMeters => Pokemon.Height / 10.0;

        /// <summary>Peso convertido de hectogramos a kilogramos.</summary>
        public double WeightInKg => Pokemon.Weight / 10.0;
    }
}
