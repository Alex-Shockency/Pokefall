using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using poke_fall_api.Enums;

namespace poke_fall_api.Models
{
    public class Pokemon
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int PokedexNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type1 { get; set; }

        public string Type2 { get; set; }

        [Required]
        public int Ability1Id { get; set; }

        public Nullable<int> Ability2Id { get; set; }

        public int HiddenAbilityId { get; set; }

        // Stored as cms and converted later
        [Required]
        public int Height { get; set; }

        // Stored as gs and converted later
        [Required]
        public int Weight { get; set; }

        [Required]
        public int BaseEXPYield { get; set; }

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
    }
}
