using System.Net;
using System.Text.Json;
using PokeApiWebApp.Models;

namespace PokeApiWebApp.Services
{
    /// <summary>
    /// Encapsula toda la comunicación HTTP con la PokéAPI (https://pokeapi.co).
    /// El HttpClient lo crea y configura IHttpClientFactory (ver Program.cs),
    /// por eso se recibe por el constructor.
    /// </summary>
    public class PokeApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PokeApiService> _logger;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public PokeApiService(HttpClient httpClient, ILogger<PokeApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// GET /pokemon?offset={offset}&amp;limit={limit}
        /// Devuelve una página del listado de Pokémon.
        /// </summary>
        public async Task<PokemonListResponse> GetPokemonListAsync(int offset, int limit)
        {
            // 1. Realizar la solicitud HTTP
            var response = await _httpClient.GetAsync($"pokemon?offset={offset}&limit={limit}");

            // 2. Verificar el código de estado (lanza HttpRequestException si no es 2xx)
            response.EnsureSuccessStatusCode();

            // 3. Leer el cuerpo de la respuesta como texto JSON
            var json = await response.Content.ReadAsStringAsync();

            // 4. Deserializar (convertir) el JSON a objetos C#
            return JsonSerializer.Deserialize<PokemonListResponse>(json, JsonOptions)
                   ?? new PokemonListResponse();
        }

        /// <summary>
        /// GET /pokemon/{nombreOId}
        /// Devuelve el detalle de un Pokémon o null si no existe (404).
        /// </summary>
        public async Task<Pokemon?> GetPokemonAsync(string nameOrId)
        {
            // La API solo acepta nombres en minúsculas y sin espacios
            var clave = Uri.EscapeDataString(nameOrId.Trim().ToLowerInvariant());

            var response = await _httpClient.GetAsync($"pokemon/{clave}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogInformation("Pokémon '{Clave}' no encontrado en la API", clave);
                return null;
            }

            response.EnsureSuccessStatusCode();

            // ReadFromJsonAsync combina la lectura y la deserialización en un solo paso
            return await response.Content.ReadFromJsonAsync<Pokemon>(JsonOptions);
        }

        /// <summary>
        /// GET /pokemon-species/{id}
        /// Información adicional (descripción, categoría). Si falla, devuelve null
        /// para que la página de detalle se muestre igualmente.
        /// </summary>
        public async Task<PokemonSpecies?> GetPokemonSpeciesAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"pokemon-species/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<PokemonSpecies>(JsonOptions);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "No se pudo obtener la especie del Pokémon {Id}", id);
                return null;
            }
        }
    }
}
