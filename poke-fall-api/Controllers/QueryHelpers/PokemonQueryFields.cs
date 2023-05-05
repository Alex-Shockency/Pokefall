namespace poke_fall_api.Controllers.QueryHelpers;

public class PokemonQueryFields
{
    public HashSet<string> Name {get; set;}
    public HashSet<string> Type {get; set;}
    public HashSet<string> Ability {get; set;}
    public Tuple<string, int> BaseStatTotal {get; set;}
}