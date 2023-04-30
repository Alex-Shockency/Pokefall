using System.Globalization;
using System.Text.RegularExpressions;
using poke_fall_api.Controllers.QueryHelpers;

namespace poke_fall_api.Utils;

public static class StringUtils
{
      public static string FirstCharToUpper(string input) {
        TextInfo textInfo = new CultureInfo("en-US",false).TextInfo;
        return textInfo.ToTitleCase(input);
      }
}