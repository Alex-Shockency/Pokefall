using System.Collections;
using System.Text.RegularExpressions;
using poke_fall_api.Controllers.QueryHelpers;

namespace poke_fall_api.Utils;

public static class QueryStringUtils
{
    private const string NAME = "Name";
    private const string TYPE = "Type";
    private const string BASE_STAT_TOTAL = "BaseStatTotal";
    private const string ABILITY = "Ability";
    private static readonly Dictionary<string, string> _searchTermMap = new Dictionary<string, string> {
        {"t", TYPE},
        {"bst", BASE_STAT_TOTAL},
        {"a", ABILITY}
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

        Dictionary<string, Tuple<string, int>> intFieldMap = new Dictionary<string, Tuple<string, int>>();
        intFieldMap.Add(BASE_STAT_TOTAL, null);
        foreach (string entry in entries) {
            if (entry.Contains(":"))
            {
                string[] pair = entry.Split(":");
                addQueryTerm(stringFieldMap, pair);
            }
            else if (entry.Contains("<") || entry.Contains(">") || entry.Contains("=")) 
            {
                string pattern = @"[><=]=?"; // Matches >, <, >=, or <=

                Match match = Regex.Match(entry, pattern);
                if (match.Success && !entry.Contains("=="))
                {
                    string op = match.Value;
                    string[] parts = entry.Split(new string[] { op }, StringSplitOptions.None);

                    if (isSearchField(parts[0]))
                    {
                        intFieldMap[parts[0]] = new Tuple<string, int>(op, Int32.Parse(parts[1]));
                    }
                }
                
            } else {
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
            BaseStatTotal = intFieldMap[BASE_STAT_TOTAL]
        };
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