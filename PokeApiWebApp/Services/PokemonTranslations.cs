namespace PokeApiWebApp.Services
{
    /// <summary>
    /// La PokéAPI devuelve los nombres de tipos y estadísticas en inglés.
    /// Esta clase los traduce al español y asigna un color a cada tipo.
    /// </summary>
    public static class PokemonTranslations
    {
        private static readonly Dictionary<string, (string Nombre, string Color)> Tipos = new()
        {
            ["normal"] = ("Normal", "#A8A77A"),
            ["fire"] = ("Fuego", "#EE8130"),
            ["water"] = ("Agua", "#6390F0"),
            ["electric"] = ("Eléctrico", "#F7D02C"),
            ["grass"] = ("Planta", "#7AC74C"),
            ["ice"] = ("Hielo", "#96D9D6"),
            ["fighting"] = ("Lucha", "#C22E28"),
            ["poison"] = ("Veneno", "#A33EA1"),
            ["ground"] = ("Tierra", "#E2BF65"),
            ["flying"] = ("Volador", "#A98FF3"),
            ["psychic"] = ("Psíquico", "#F95587"),
            ["bug"] = ("Bicho", "#A6B91A"),
            ["rock"] = ("Roca", "#B6A136"),
            ["ghost"] = ("Fantasma", "#735797"),
            ["dragon"] = ("Dragón", "#6F35FC"),
            ["dark"] = ("Siniestro", "#705746"),
            ["steel"] = ("Acero", "#B7B7CE"),
            ["fairy"] = ("Hada", "#D685AD"),
        };

        private static readonly Dictionary<string, string> Estadisticas = new()
        {
            ["hp"] = "PS",
            ["attack"] = "Ataque",
            ["defense"] = "Defensa",
            ["special-attack"] = "Ataque Esp.",
            ["special-defense"] = "Defensa Esp.",
            ["speed"] = "Velocidad",
        };

        public static string TypeName(string type) =>
            Tipos.TryGetValue(type, out var t) ? t.Nombre : type;

        public static string TypeColor(string type) =>
            Tipos.TryGetValue(type, out var t) ? t.Color : "#777777";

        public static string StatName(string stat) =>
            Estadisticas.TryGetValue(stat, out var nombre) ? nombre : stat;

        /// <summary>"mr-mime" → "Mr Mime"</summary>
        public static string DisplayName(string apiName) =>
            string.Join(' ', apiName.Split('-', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => char.ToUpperInvariant(p[0]) + p[1..]));
    }
}
