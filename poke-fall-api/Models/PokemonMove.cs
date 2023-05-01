using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

[Keyless]
public class PokemonMove
{
    [Required]
    public int PokemonId { get; set; }
    [Required]
    public int MoveId { get; set; }
    [Required]
    public int VersionId { get; set; }
    [Required]
    public bool LevelUpLearnable { get; set; }
    public Nullable<int> LevelLearned { get; set; }
    [Required]
    public bool TMLearnable { get; set; }
    [Required]
    public bool TutorLearnable { get; set; }
    [Required]
    public bool EggMoveLearnable { get; set; }

}
