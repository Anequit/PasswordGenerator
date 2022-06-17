using PasswordGenerator.Core.DataModels;
using System;

namespace PasswordGenerator.Core.Helpers;

public static class PartHelper
{
    public static Part[] InitializePartArray(int length)
    {
        int buffer = 1000;

        int amount = (int)Math.Ceiling((double)length / buffer);

        Part[] parts = new Part[amount];

        for(int x = 0; x < amount; x++)
        {
            parts[x] = new Part(buffer)
            {
                StartingBounds = x * buffer,
                EndingBounds = (x * buffer) + buffer
            };
        }

        parts[^1].EndingBounds = length;

        return parts;
    }
}
