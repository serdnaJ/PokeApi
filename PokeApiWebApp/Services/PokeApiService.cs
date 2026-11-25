using System.Net;
using System.Text.Json;
using PokeApiWebApp.Models;

namespace PokeApiWebApp.Services
{
    /// Encapsula toda la comunicación HTTP con la PokéAPI (https://pokeapi.co). y envia
   
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

    
       
        /// Devuelve una página del listado de Pokémon.
        
        public async Task<PokemonListResponse> GetPokemonListAsync(int offset, int limit)
        {
          
            var response = await _httpClient.GetAsync($"pokemon?offset={offset}&limit={limit}");

            
            response.EnsureSuccessStatusCode();

            
            var json = await response.Content.ReadAsStringAsync();

            
            return JsonSerializer.Deserialize<PokemonListResponse>(json, JsonOptions)
                   ?? new PokemonListResponse();
        }

       
        
        /// Devuelve el detalle de un Pokémon o null si no existe (404).
        
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

            //  combina la lectura y la deserialización en un solo paso
            return await response.Content.ReadFromJsonAsync<Pokemon>(JsonOptions);
        }

        
      
        /// Información adicional 
        
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
