using Microsoft.EntityFrameworkCore;

namespace poke_fall_api.DTO
{
    [Keyless]
    public class SearchResultPokemonDTO
    {
        public int Id { get; set; }
        public int PokedexNumber { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
    }
}