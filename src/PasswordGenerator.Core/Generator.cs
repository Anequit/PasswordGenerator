using PasswordGenerator.Core.Helpers;
using System;
using System.Text;

namespace PasswordGenerator.Core;

public static class Generator
{
    public static string GeneratePassword(in int length, in bool symbols, in bool numbers)
    {
        if(length > 1_000_000_000)
            return "Length too long";

        ReadOnlySpan<char> pool = BuildPool(symbols, numbers);

        int[] partArray = PartHelper.InitializePartArray(length);

        StringBuilder builder = new StringBuilder(length);

        foreach (int partLength in partArray)
        {
            GeneratePartData(builder, partLength, pool);
        }
        
        GC.Collect(); // Cleanup any allocated string builders from GeneratePartData

        return builder.ToString();
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

    private static void GeneratePartData(in StringBuilder builder, in int length, in ReadOnlySpan<char> pool)
    {
        for (int x = 0; x < length; x++)
        {
            builder.Append(pool[Random.Shared.Next(0, pool.Length)]);
        }
    }
}
