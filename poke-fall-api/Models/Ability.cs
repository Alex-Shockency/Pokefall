using poke_fall_api.Enums;
using System.ComponentModel.DataAnnotations;

namespace poke_fall_api.Models
{
    public class Ability{
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }  
}