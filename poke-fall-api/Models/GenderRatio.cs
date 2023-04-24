using Microsoft.EntityFrameworkCore;
using poke_fall_api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace poke_fall_api.Models{
    [NotMapped]
    public class GenderRatio{
        [Required]
        public int Male { get; set; }
         [Required]
        public int Female { get; set; }
         [Required]
        public int Genderless { get; set; } 
    } 
}