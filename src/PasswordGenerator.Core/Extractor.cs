using System.Text;

namespace PasswordGenerator.Core;

public static class Extractor
{
    static readonly char[] _symbols = { '/', '+', '=' };
    static readonly char[] _numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    public static StringBuilder ExtractSymbols(StringBuilder data)
    {
        foreach(char c in _symbols)
            data.Replace(c.ToString(), string.Empty);

        return data;
    }

    public static StringBuilder ExtractNumbers(StringBuilder data)
    {
        foreach(char c in _numbers)
            data.Replace(c.ToString(), string.Empty);

        return data;
    }
}
