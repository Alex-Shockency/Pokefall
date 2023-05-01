public class FullMoveInfo
{
    public int PokemonId { get; set; }

    public int MoveId { get; set; }
     public int VersionId { get; set; }
    public bool LevelUpLearnable { get; set; }

    public Nullable<int> LevelLearned { get; set; }

    public bool TMLearnable { get; set; }

    public bool TutorLearnable { get; set; }

    public bool EggMoveLearnable { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Type { get; set; }

    public string Category { get; set; }

    public int PP { get; set; }

    public int Power { get; set; }

    public int Accuracy { get; set; }

    public bool Contact { get; set; }

    public bool AffectedProtect { get; set; }

    public bool AffectedSnatch { get; set; }

    public bool AffectedMirrorMove { get; set; }
}