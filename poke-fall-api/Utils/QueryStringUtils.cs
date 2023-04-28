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
                            
        Dictionary<string, string> stringFieldMap = new Dictionary<string, string>();
        HashSet<string> nameEntries = new HashSet<string>();
        Dictionary<string, Tuple<string, int>> intFieldMap = new Dictionary<string, Tuple<string, int>>();
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
                        intFieldMap.Add(_searchTermMap[parts[0]], new Tuple<string, int>(op, Int32.Parse(parts[1])));
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
            Name = nameEntries.Count() > 0 ? String.Join(" ", nameEntries) : String.Empty,
            Type = stringFieldMap.ContainsKey(TYPE) ? stringFieldMap[TYPE] : String.Empty,
            Ability = stringFieldMap.ContainsKey(ABILITY) ? stringFieldMap[ABILITY] : String.Empty,
            BaseStatTotal = intFieldMap.ContainsKey(BASE_STAT_TOTAL) ? intFieldMap[BASE_STAT_TOTAL] : null
        };
    }

    private static void addQueryTerm(Dictionary<string, string> fieldMap, string[] pair)
    {
        // TODO(akmindt): What happens if we want to search for Pokemon with multiple types or abilities etc etc
        if (isSearchField(pair[0]))
        {
            fieldMap.Add(_searchTermMap[pair[0]], pair[1]);
        }
    }

    private static Boolean isSearchField(string key) {
        return _searchTermMap.ContainsKey(key);
    }
}