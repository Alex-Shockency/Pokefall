using Microsoft.EntityFrameworkCore;
using poke_fall_api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace poke_fall_api.Models{
    [NotMapped]
    public class LevelUpMove : Move{
        [Required]
        public int Level { get; set; }
    } 
}