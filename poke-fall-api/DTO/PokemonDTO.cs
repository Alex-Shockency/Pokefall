using Microsoft.EntityFrameworkCore;
using poke_fall_api.Models;

namespace poke_fall_api.DTO
{
    [Keyless]
    public class PokemonDTO
    {
        public int PokemonId { get; set; }
        public int PokedexNumber { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public int Ability1Id { get; set; }
        public int Ability2Id { get; set; }
        public int HiddenAbilityId { get; set; }
        public int CatchRate { get; set; }
        public List<GenderRatio> GenderRatio { get; set; }
        public string EggGroup { get; set; }
        public int HatchStepsMin { get; set; }
        public int HatchStepMax { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int BaseEXPYield { get; set; }
        public string LevelingRate { get; set; }
        public Stats EVYield { get; set; }
        public int BaseFriendship { get; set; }
        public Stats BaseStats { get; set; }
        public List<LevelUpMove> LevelUpMoves { get; set; }
        public List<Move> TMMoves { get; set; }
        public List<Move> EggMoves { get; set; }
        public List<Move> TutorMoves { get; set; }
        public int EvolvesTo { get; set; }
        public int EvolvesFrom { get; set; }
        public string? EvolutionDescription { get; set; }
    }
}