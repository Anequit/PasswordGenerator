using PasswordGenerator.Core.DataModels;
using PasswordGenerator.Core.Helpers;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGenerator.Core;

public static class Generator
{
    public static string GeneratePassword(int length, bool symbols, bool numbers)
    {
        StringBuilder sb = new StringBuilder(length);

        Part[] parts = PartHelper.InitializePartArray(length);

        Parallel.ForEach(parts, part =>
        {
            part.Data.Append(GeneratePassword());

            do
            {
                if(symbols == false)
                    part.Data = Extractor.ExtractSymbols(part.Data);

                if(numbers == false)
                    part.Data = Extractor.ExtractNumbers(part.Data);

                if(part.ActualLength > part.DesiredLength)
                    part.Data.Remove(part.DesiredLength, part.ActualLength - part.DesiredLength);

                if(part.ActualLength < part.DesiredLength)
                    part.Data.Append(GeneratePassword());

            } while(part.Data.Length != part.DesiredLength);
        });

        foreach(Part part in parts)
            sb.Append(part.Data);
        
        return sb.ToString();
    }

    private static string GeneratePassword() => Convert.ToBase64String(SHA256.HashData(GenerateGuidByteArray().Concat(GenerateGuidByteArray()).ToArray()));

    private static byte[] GenerateGuidByteArray() => Guid.NewGuid().ToByteArray();
}
