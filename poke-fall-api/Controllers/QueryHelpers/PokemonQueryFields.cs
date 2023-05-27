namespace poke_fall_api.Controllers.QueryHelpers;

public class PokemonQueryFields
{
    public HashSet<string> Name {get; set;}
    public HashSet<string> Type {get; set;}
    public HashSet<string> Ability {get; set;}
    public Tuple<string, int> BaseStatTotal {get; set;}
    public Tuple<string, int> HP {get; set;}
    public Tuple<string, int> Attack {get; set;}
    public Tuple<string, int> Defense {get; set;}
    public Tuple<string, int> SpecialAttack {get; set;}
    public Tuple<string, int> SpecialDefense {get; set;}
    public Tuple<string, int> Speed {get; set;}
}