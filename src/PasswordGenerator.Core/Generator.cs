using System;
using System.Text;

namespace PasswordGenerator.Core;

public static class Generator
{
    public static string GeneratePassword(in int length, in bool symbols, in bool numbers)
    {
        if(length > 1_000_000_000)
            return "Length too long";

        return GeneratePassword(length, BuildPool(symbols, numbers));
    }

    private static ReadOnlySpan<char> BuildPool(in bool symbols, in bool numbers)
    {
        string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        if (symbols)
            pool += "!@#$%^&*()_+-=[]{}:;<>?/`~";

        if (numbers)
            pool += "1234567890";

        return pool.AsSpan();
    }

    private static string GeneratePassword(in int length, in ReadOnlySpan<char> pool)
    {
        StringBuilder builder = new StringBuilder(length);
        
        for (int x = 0; x < length; x++)
        {
            builder.Append(pool[Random.Shared.Next(0, pool.Length)]);
        }
        
        return builder.ToString();
    }
}
