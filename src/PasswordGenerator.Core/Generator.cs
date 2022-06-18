using PasswordGenerator.Core.DataModels;
using PasswordGenerator.Core.Helpers;
using System;
using System.Text;
using System.Threading;
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

        return PartHelper.CombinePartData(parts).ToString();
    }

    public static async Task<string> GeneratePasswordAsync(int length, bool symbols, bool numbers)
    {
        string pool = BuildPool(symbols, numbers);

        Part[] parts = PartHelper.InitializePartArray(length);

        await Parallel.ForEachAsync(parts, async (part, ct) => 
        {
            await Task.Run(() =>
            {
                do
                {
                    part.Data.Append(pool[Random.Shared.Next(0, pool.Length)]);
                } while(part.Data.Length != part.DesiredLength);
            }, ct);
        });

        return PartHelper.CombinePartData(parts).ToString();
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
