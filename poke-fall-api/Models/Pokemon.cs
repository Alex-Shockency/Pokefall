using System.ComponentModel.DataAnnotations;
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

        // Stored as cms and converted later
        [Required]
        public int HeightMetric { get; set; }

        // Stored as gs and converted later
        [Required]
        public int WeightMetric { get; set; }

        [Required]
        public int BaseEXPYield { get; set; }

        [Required]
        public string LevelingRate { get; set; }

        [Required] 
        public Array EVYield { get; set; }

        [Required]
        public int BaseFriendship { get; set; }

        [Required]
        public Array BaseStats { get; set; }

        [Required]
        public List<(string, int)> LevelUpMoves { get; set; }

        [Required]
        public Array TMMoves { get; set; }

        [Required]
        public Array EggMoves { get; set; }

        [Required]
        public Array TutorMoves { get; set; }

        [Required]
        public int EvolvesTo { get; set; }

        [Required]
        public int EvolvesFrom { get; set; }

        public string? EvolutionDescription { get; set; }
    }
}