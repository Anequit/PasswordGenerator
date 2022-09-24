using PasswordGenerator.Core.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordGenerator.Core.Extensions;

internal static class StringBuilderExtensions
{
    public static void AppendPartList(this StringBuilder builder, ref List<Part> parts)
    {
        while(parts.Count > 0)
        {
            builder.Append(parts[0].Data);
            parts.RemoveAt(0);
        }

        GC.Collect();
    }
}
