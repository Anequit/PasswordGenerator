using NUnit.Framework;
using PasswordGenerator.Core.Helpers;

namespace PasswordGenerator.Core.Tests.Helpers;

public class PartHelperTests
{
    [TestCase(2130012, 3)]
    [TestCase(556565512, 1)]
    [TestCase(11212, 1)]
    public void PartHelper_CorrectLength(int length, long expected)
    {
        int result = PartHelper.InitializePartArray(length).Length;

        Assert.That(result == expected);
    }
}
