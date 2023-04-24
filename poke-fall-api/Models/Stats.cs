using Microsoft.EntityFrameworkCore;
using poke_fall_api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace poke_fall_api.Models
{
    [NotMapped]
    public class Stats{
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