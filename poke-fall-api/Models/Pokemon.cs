using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using poke_fall_api.Enums;

namespace poke_fall_api.Models
{
    public class Pokemon
    {
        [Key]
        [Required]
        public int PokemonId { get; set; }

        [Required]
        public int PokedexNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type1 { get; set; }

        public string Type2 { get; set; }

        [Required]
        public int Ability1Id { get; set; }

        public int Ability2Id { get; set; }

        public int HiddenAbilityId { get; set; }

        // [Required]
        // public int CatchRate { get; set; }

        // //public ICollection<GenderRatio> GenderRatio { get; set; }

        // [Required]
        // public PokemonEggGroup EggGroup { get; set; }

        // [Required]
        // public int HatchStepsMin { get; set; }

        // [Required]
        // public int HatchStepMax { get; set; }

        // Stored as cms and converted later
        [Required]
        public int Height { get; set; }

        // Stored as gs and converted later
        [Required]
        public int Weight { get; set; }

        [Required]
        public int BaseEXPYield { get; set; }

        // [Required]
        // public string LevelingRate { get; set; }

        // [Required]
        // public Stats EVYield { get; set; }

        // [Required]
        // public int BaseFriendship { get; set; }

        [Required]
        public int HP { get; set; }

        [Required]
        public int Attack { get; set; }

        [Required]
        public int Defense { get; set; }

        [Required]
        public int SpecAttack { get; set; }

        [Required]
        public int SpecDefense { get; set; }

        [Required]
        public int Speed { get; set; }

        // [Required]
        // public int EvolvesTo { get; set; }

        // [Required]
        // public int EvolvesFrom { get; set; }

        // public string? EvolutionDescription { get; set; }
    }
}
