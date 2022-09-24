using PasswordGenerator.Core.DataModels;
using System;

namespace PasswordGenerator.Core.Helpers;

public static class PartHelper
{
    public static Part[] InitializePartArray(in int length)
    {
        int partSize = 1024 * 1024;

        int amount = (int)Math.Ceiling((double)length / partSize);

        Part[] parts = new Part[amount];

        for(int x = 0; x < amount; x++)
        {
            parts[x] = new Part()
            {
                StartingBounds = x * partSize,
                EndingBounds = (x * partSize) + partSize
            };
        }

        parts[^1].EndingBounds = length;

        return parts;
    }
}
