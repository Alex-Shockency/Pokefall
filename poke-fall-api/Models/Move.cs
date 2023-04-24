using poke_fall_api.Enums;
using System.ComponentModel.DataAnnotations;

namespace poke_fall_api.Models
{
    public class Move{
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }

         [Required]
        public string Category { get; set; }

         [Required]
        public int PP { get; set; }

        [Required]
        public int Power { get; set; }

         [Required]
        public int Accuracy { get; set; }

         [Required]
        public bool Contact { get; set; }

         [Required]
        public bool AffectedProtect { get; set; }


        //  [Required]
        // public bool AffectedMagicCoat { get; set; }

         [Required]
        public bool AffectedSnatch { get; set; }

         [Required]
        public bool AffectedMirrorMove { get; set; }

        //   [Required]
        // public bool AffectedKingsRock { get; set; }

    }  
}