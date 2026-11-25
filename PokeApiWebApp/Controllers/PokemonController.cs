using Microsoft.AspNetCore.Mvc;
using PokeApiWebApp.Services;
using PokeApiWebApp.ViewModels;

namespace PokeApiWebApp.Controllers
{
    public class PokemonController : Controller
    {
        private const int PageSize = 20;

        private readonly PokeApiService _service;
        private readonly ILogger<PokemonController> _logger;

        // El servicio llega por inyección de dependencias (registrado en Program.cs)
        public PokemonController(PokeApiService service, ILogger<PokemonController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET /Pokemon?page=1
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) page = 1;

            try
            {
                var offset = (page - 1) * PageSize;
                var data = await _service.GetPokemonListAsync(offset, PageSize);

                var model = new PokemonListViewModel
                {
                    Pokemons = data.Results,
                    CurrentPage = page,
                    TotalCount = data.Count,
                    TotalPages = (int)Math.Ceiling(data.Count / (double)PageSize)
                };

                return View(model);
            }
            catch (HttpRequestException ex)
            {
                return ApiError(ex);
            }
        }

        // GET /Pokemon/Details/pikachu  o  /Pokemon/Details/25
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var pokemon = await _service.GetPokemonAsync(id);
                if (pokemon == null)
                {
                    ViewBag.Query = id;
                    Response.StatusCode = StatusCodes.Status404NotFound;
                    return View("NotFound");
                }

                var species = await _service.GetPokemonSpeciesAsync(pokemon.Id);

                var model = new PokemonDetailViewModel
                {
                    Pokemon = pokemon,
                    // Tomamos la última descripción disponible en español
                    Description = species?.FlavorTextEntries
                        .LastOrDefault(f => f.Language.Name == "es")?.FlavorText
                        .Replace('\n', ' ').Replace('\f', ' '),
                    Genus = species?.Genera
                        .FirstOrDefault(g => g.Language.Name == "es")?.Name
                };

                return View(model);
            }
            catch (HttpRequestException ex)
            {
                return ApiError(ex);
            }
        }

        // GET /Pokemon/Search?query=charizard
        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Details), new { id = query.Trim().ToLowerInvariant() });
        }

        private IActionResult ApiError(HttpRequestException ex)
        {
            _logger.LogError(ex, "Error al consumir la PokéAPI");
            ViewBag.Message = ex.Message;
            Response.StatusCode = StatusCodes.Status502BadGateway;
            return View("ApiError");
        }
    }
}
