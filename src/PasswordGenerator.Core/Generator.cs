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
        if(length > int.MaxValue)
            return "Length too long";

        string pool = BuildPool(symbols, numbers);

        Part[] parts = PartHelper.InitializePartArray(length);
        
        StringBuilder password = new StringBuilder(length);

        Parallel.ForEach(parts, part =>
        {
            GeneratePartData(ref part, pool);
        });

        PartHelper.CombinePartData(ref password, parts);

        return password.ToString();;
    }

    public static async Task<string> GeneratePasswordAsync(int length, bool symbols, bool numbers) => await Task.Run(() => GeneratePassword(length, symbols, numbers));

    private static string BuildPool(bool symbols, bool numbers)
    {
        string pool = _letters;

        if (symbols)
            pool += _symbols;

        if (numbers)
            pool += _numbers;

        return pool;
    }

    private static void GeneratePartData(ref Part part, string pool)
    {
        do
        {
            part.Data.Append(pool[Random.Shared.Next(0, pool.Length)]);
        } while(part.Data.Length != part.DesiredLength);
    }
}
