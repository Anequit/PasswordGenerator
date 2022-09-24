using PasswordGenerator.Core.DataModels;
using PasswordGenerator.Core.Extensions;
using PasswordGenerator.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGenerator.Core;

public static class Generator
{
    const string _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    const string _symbols = "!@#$%^&*()_+-=[]{}:;<>?/`~";
    const string _numbers = "1234567890";

    public static string GeneratePassword(in int length, in bool symbols, in bool numbers)
    {
        if(length > int.MaxValue)
            return "Length too long";

        char[] pool = BuildPool(symbols, numbers).ToCharArray();

        List<Part> parts = PartHelper.InitializePartArray(length).ToList();
        
        StringBuilder password = new StringBuilder(length);

        Parallel.ForEach(parts, part =>
        {
            GeneratePartData(ref part, pool);
        });

        password.AppendPartList(ref parts);

        return password.ToString();
    }

    public static async Task<string> GeneratePasswordAsync(int length, bool symbols, bool numbers) => await Task.Run(() => GeneratePassword(length, symbols, numbers));

    private static string BuildPool(in bool symbols, in bool numbers)
    {
        string pool = _letters;

        if (symbols)
            pool += _symbols;

        if (numbers)
            pool += _numbers;

        return pool;
    }

    private static void GeneratePartData(ref Part part, in char[] pool)
    {
        StringBuilder builder = new StringBuilder(part.GetDesiredLength(), part.GetDesiredLength());

        do
        {
            builder.Append(pool[Random.Shared.Next(0, pool.Length)]);
        } while(builder.Length < builder.MaxCapacity);

        part.Data = builder.ToString();
    }
}
