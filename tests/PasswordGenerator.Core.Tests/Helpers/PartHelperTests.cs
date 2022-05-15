using NUnit.Framework;
using PasswordGenerator.Core.Helpers;

namespace PasswordGenerator.Core.Tests.Helpers;

public class PartHelperTests
{
    [TestCase(2130012, 2131)]
    [TestCase(556565512, 556566)]
    [TestCase(11212, 12)]
    public void PartHelper_CorrectLength(int length, long expected)
    {
        int result = PartHelper.InitializePartArray(length, 1000).Length;

        Assert.That(result == expected);
    }
}
