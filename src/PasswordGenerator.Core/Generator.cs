using PasswordGenerator.Core.Helpers;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGenerator.Core;

public static class Generator
{
    public static string GeneratePassword(in int length, in bool symbols, in bool numbers)
    {
        if(length > 100000000)
            return "Length too long";

        char[] pool = BuildPool(symbols, numbers);

        int[] partArray = PartHelper.InitializePartArray(length, 1024 * 1024);

        StringBuilder builder = new StringBuilder(length);

        Parallel.ForEach(partArray, part =>
        {
            lock(builder)
                builder.Append(GeneratePartData(part, pool));
        });

        GC.Collect(); // Cleanup any allocated string builders from GeneratePartData

        return builder.ToString();
    }

    private static char[] BuildPool(in bool symbols, in bool numbers)
    {
        string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        if (symbols)
            pool += "!@#$%^&*()_+-=[]{}:;<>?/`~";

        if (numbers)
            pool += "1234567890";

        return pool.ToCharArray();
    }

    private static string GeneratePartData(in int length, in char[] pool)
    {
        StringBuilder builder = new StringBuilder(length);

        do
        {
            builder.Append(pool[Random.Shared.Next(0, pool.Length)]);
        } while(builder.Length < length);

        return builder.ToString();
    }
}
