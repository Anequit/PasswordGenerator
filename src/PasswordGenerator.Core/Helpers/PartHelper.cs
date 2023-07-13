using System;

namespace PasswordGenerator.Core.Helpers;

public static class PartHelper
{
    private static readonly int _partSize = 1048576;

    public static int[] InitializePartArray(in int length)
    {
        int amount = (int)Math.Ceiling((double)length / _partSize);

        int[] parts = new int[amount];

        Array.Fill(parts, _partSize);
        
        parts[^1] = length - ((amount - 1) * _partSize);

        return parts;
    }
}
