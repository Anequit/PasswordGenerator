using PasswordGenerator.Core.DataModels;
using PasswordGenerator.Core.Helpers;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGenerator.Core;

public static class Generator
{
    const string _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    const string _symbols = "!@#$%^&*()_+-=[]{}:;<>?/`~";
    const string _numbers = "12345678990";

    public static string GeneratePassword(int length, bool symbols, bool numbers)
    {
        string pool = BuildPool(symbols, numbers);

        Part[] parts = PartHelper.InitializePartArray(length);

        Parallel.ForEach(parts, part =>
        {
            do
            {
                part.Data.Append(pool[Random.Shared.Next(0, pool.Length)]);
            } while(part.Data.Length != part.DesiredLength);
        });

        StringBuilder sb = new StringBuilder(length);

        foreach (Part part in parts)
            sb.Append(part.Data);
        
        return sb.ToString();
    }

    private static string BuildPool(bool symbols, bool numbers)
    {
        string pool = _letters;

        if (symbols)
            pool += _symbols;

        if (numbers)
            pool += _numbers;

        return pool;
    }
}
