using Microsoft.EntityFrameworkCore;

namespace poke_fall_api.DTO
{
    [Keyless]
    public class PokemonDTO
    {
        public int Id { get; set; }
        public Nullable<int> EvolutionChainId { get; set; }
        public int PokedexNumber { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public int Ability1Id { get; set; }
        public Nullable<int> Ability2Id { get; set; }
        public int HiddenAbilityId { get; set; }
        // Stored as cms and converted later
        public int Height { get; set; }
        // Stored as gs and converted later
        public int Weight { get; set; }
        public int BaseEXPYield { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecAttack { get; set; }
        public int SpecDefense { get; set; }
        public int Speed { get; set; }
        public bool IsBaby { get; set; }
    }
}