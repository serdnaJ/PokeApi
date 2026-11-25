using PokeApiWebApp.Models;

namespace PokeApiWebApp.ViewModels
{
    /// <summary>
    /// Datos que la vista del listado necesita: los Pokémon de la página actual
    /// y la información de paginación.
    /// </summary>
    public class PokemonListViewModel
    {
        public List<NamedApiResource> Pokemons { get; set; } = new();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
