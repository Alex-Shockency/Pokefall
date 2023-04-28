namespace poke_fall_api.Controllers.QueryHelpers;

public class PokemonQueryFields
{
    public string Name {get; set;}
    public string Type {get; set;}
    public string Ability {get; set;}
    public Tuple<string, int> BaseStatTotal {get; set;}
}