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

        // lazily assume name search if single search term without a colon
        if (entries.Count() == 1 && ! entries.First().Contains(":")) {
            return new PokemonQueryFields {
                Name = queryString,
                Type = ""
            };
        }
                            
        Dictionary<string, string> fieldMap = new Dictionary<string, string>();
        HashSet<string> nameEntries = new HashSet<string>();
        foreach (string entry in entries) {
            if (entry.Contains(":")) {
                string[] pair = entry.Split(":");
                if (isSearchField(pair[0])) {
                    fieldMap.Add(_searchTermMap[pair[0]], pair[1]);
                }
            } else {
                // lazily assume this is looking for a name
                nameEntries.Add(entry);
            }
        }
       

        // TODO(aaron) make name return an array to chain like statements
        return new PokemonQueryFields{
            Name = nameEntries.Count() > 0 ? String.Join(" ", nameEntries) : String.Empty,
            Type = fieldMap.ContainsKey(TYPE) ? fieldMap[TYPE] : String.Empty
        };
    }

    private static Boolean isSearchField(string key) {
        return _searchTermMap.ContainsKey(key);
    }
}