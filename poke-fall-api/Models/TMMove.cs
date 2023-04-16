using System.ComponentModel.DataAnnotations;

namespace poke_fall_api.Models
{
    public class TMMove
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PokemonId { get; set; }
        [Required]
        public int MoveId { get; set; }
    }
}