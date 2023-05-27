using System.Collections;
using System.Text.RegularExpressions;
using poke_fall_api.Controllers.QueryHelpers;

namespace poke_fall_api.Utils;

public static class QueryStringUtils
{
    // STRING TERMS
    private const string NAME = "Name";
    private const string TYPE = "Type";
    private const string ABILITY = "Ability";

    // INTEGER TERMS
    private const string BASE_STAT_TOTAL = "BaseStatTotal";
    private const string HP = "HP";
    private const string ATTACK = "Attack";
    private const string DEFENSE = "Defense";
    private const string SPECIAL_ATTACK = "SpecialAttack";
    private const string SPECIAL_DEFENSE = "SPecialDefense";
    private const string SPEED = "Speed";
    
    private static readonly Dictionary<string, string> _searchTermMap = new Dictionary<string, string> {
        // STRING TERMS
        {"t", TYPE},
        {"a", ABILITY},

        // INTEGER TERMS
        {"bst", BASE_STAT_TOTAL},
        {"hp", HP},
        {"atk", ATTACK},
        {"def", DEFENSE},
        {"spa", SPECIAL_ATTACK},
        {"spdef", SPECIAL_DEFENSE},
        {"spd", SPEED}
    };

    public static PokemonQueryFields readQueryString(string queryString) {

        Regex regex = new Regex("(?:[^\\s\"]+|\"[^\"]*\")+");

        HashSet<string> entries = regex.Matches(queryString)
                            .Cast<Match>()
                            .Select(match => match.Value)
                            .ToHashSet();
        Dictionary<string, HashSet<string>> stringFieldMap = new Dictionary<string, HashSet<string>>();
        HashSet<string> nameEntries = new HashSet<string>();
        HashSet<string> typeEntries = new HashSet<string>();
        HashSet<string> abilityEntries = new HashSet<string>();
        stringFieldMap.Add(NAME, nameEntries);
        stringFieldMap.Add(TYPE, typeEntries);
        stringFieldMap.Add(ABILITY, abilityEntries);

        Dictionary<string, Tuple<string, int>> intFieldMap = new Dictionary<string, Tuple<string, int>>{
            {BASE_STAT_TOTAL, null},
            {HP, null},
            {ATTACK, null},
            {DEFENSE, null},
            {SPECIAL_ATTACK, null},
            {SPECIAL_DEFENSE, null},
            {SPEED, null}
        };
        foreach (string entry in entries) {
            if (entry.Contains(":"))
            {
                string[] pair = entry.Split(":");
                addQueryTerm(stringFieldMap, pair);
            }
            else if (entry.Contains("<") || entry.Contains(">") || entry.Contains("="))
            {
                addIntegerField(intFieldMap, entry);
            }
            else {
                // lazily assume this is looking for a name
                nameEntries.Add(entry);
            }
        }
       

        // TODO(aaron): make name return an array to chain like statements
        return new PokemonQueryFields 
        {
            Name = stringFieldMap[NAME],
            Type = stringFieldMap[TYPE],
            Ability = stringFieldMap[ABILITY],
            BaseStatTotal = intFieldMap[BASE_STAT_TOTAL],
            HP = intFieldMap[HP],
            Attack = intFieldMap[ATTACK],
            Defense = intFieldMap[DEFENSE],
            SpecialAttack = intFieldMap[SPECIAL_ATTACK],
            SpecialDefense = intFieldMap[SPECIAL_DEFENSE],
            Speed = intFieldMap[SPEED]
        };
    }

    private static void addIntegerField(Dictionary<string, Tuple<string, int>> intFieldMap, string entry)
    {
        string pattern = @"[><=]=?"; // Matches >, <, >=, or <=

        Match match = Regex.Match(entry, pattern);
        if (match.Success && !entry.Contains("=="))
        {
            string op = match.Value;
            string[] parts = entry.Split(new string[] { op }, StringSplitOptions.None);

            if (isSearchField(parts[0]))
            {
                intFieldMap[_searchTermMap[parts[0]]] = new Tuple<string, int>(op, Int32.Parse(parts[1]));
            }
        }
    }

    private static void addQueryTerm(Dictionary<string, HashSet<string>> fieldMap, string[] pair)
    {
        // TODO(akmindt): What happens if we want to search for Pokemon with multiple types or abilities etc etc
        if (isSearchField(pair[0]))
        {

            if(TYPE.Equals(_searchTermMap[pair[0]]) && fieldMap[TYPE].Count() > 2) {
                return;
            }
            fieldMap[_searchTermMap[pair[0]]].Add(pair[1]);
        }
    }

    private static Boolean isSearchField(string key) {
        return _searchTermMap.ContainsKey(key);
    }
}