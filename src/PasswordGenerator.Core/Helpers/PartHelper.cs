using System;

namespace PasswordGenerator.Core.Helpers;

public static class PartHelper
{
    public static int[] InitializePartArray(in int length, in int partSize)
    {
        int amount = (int)Math.Ceiling((double)length / partSize);

        int[] parts = new int[amount];

        for(int x = 0; x < amount; x++)
        {
            parts[x] = partSize;
        }

        parts[^1] = length - ((amount - 1) * partSize);

        return parts;
    }
}
