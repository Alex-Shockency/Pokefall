using Microsoft.EntityFrameworkCore;
using poke_fall_api.Enums;

namespace poke_fall_api.Models
{

    public class Pokemon
    {
        [Required]
        public int PokemonId { get; set; }

        [Required]
        public int PokedexNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public PokemonType Type1 { get; set; }

        public PokemonType Type2 { get; set; }

        [Required]
        public int Ability1Id { get; set; }

        public int Ability2Id { get; set; }

        public int HiddenAbilityId { get; set; }

        [Required]
        public int CatchRate { get; set; }

        public List<(string,  int)> GenderRatio { get; set; }

        [Required]
        public PokemonEggGroup EggGroup { get; set; }

        [Required]
        public int HatchStepsMin { get; set; }

        [Required]
        public int HatchStepMax { get; set; }

        [Required]
        public string HeightImperial { get; set; }

        [Required]
        public string HeightMetric { get; set; }

        [Required]
        public string WeightImperial { get; set; }

        [Required]
        public string WeightMetric { get; set; }

        [Required]
        public int BaseEXPYield { get; set; }

        [Required]
        public string LevelingRate { get; set; }

        [Required] 
        public Array<int> EVYield { get; set; }

        [Required]
        public int BaseFriendship { get; set; }

        [Required]
        public Array<int> BaseStats { get; set; }

        [Required]
        public List<(string, int)> LevelUpMoves { get; set; }

        [Required]
        public Array<string> TMMoves { get; set; }

        [Required]
        public Array<string> EggMoves { get; set; }

        [Required]
        public Array<string> TutorMoves { get; set; }

        [Required]
        public string EvolutionDescription { get; set; }
    }
}