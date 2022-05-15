using NUnit.Framework;
using System.Threading.Tasks;

namespace PasswordGenerator.Core.Tests;

public class GeneratorTests
{
    [TestCase(1032082, true, true)]
    [TestCase(3123901, false, true)]
    [TestCase(3123550, true, false)]
    [TestCase(5555555, false, false)]
    public void Generator_CorrectLength(int length, bool symbols, bool numbers)
    {
        string result = Generator.GeneratePassword(length, symbols, numbers);

        Assert.That(result.Length == length);
    }
}
