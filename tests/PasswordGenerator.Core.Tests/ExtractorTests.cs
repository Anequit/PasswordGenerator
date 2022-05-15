using NUnit.Framework;
using System.Text;

namespace PasswordGenerator.Core.Tests;

public class ExtractorTests
{
    [TestCase("N4WHG1PcEyvXdrm/SBsjCMmEkLQNFdGTpMVwTELp/8k=", "N4WHG1PcEyvXdrmSBsjCMmEkLQNFdGTpMVwTELp8k")]
    [TestCase("pqDYb7YEIyraRh7IHzOf47dXVeZ0Dol+v30pedQ4YOY=", "pqDYb7YEIyraRh7IHzOf47dXVeZ0Dolv30pedQ4YOY")]
    [TestCase("OfVS5WQYQ3bV5Q/V9alVyUw8Uep15GFZ4AD2WJALdLY=", "OfVS5WQYQ3bV5QV9alVyUw8Uep15GFZ4AD2WJALdLY")]
    public void ExtractSymbols(string data, string expected)
    {
        string result = Extractor.ExtractSymbols(new StringBuilder(data)).ToString();

        Assert.That(result == expected);
    }

    [TestCase("N4WHG1PcEyvXdrm/SBsjCMmEkLQNFdGTpMVwTELp/8k=", "NWHGPcEyvXdrm/SBsjCMmEkLQNFdGTpMVwTELp/k=")]
    [TestCase("pqDYb7YEIyraRh7IHzOf47dXVeZ0Dol+v30pedQ4YOY=", "pqDYbYEIyraRhIHzOfdXVeZDol+vpedQYOY=")]
    [TestCase("OfVS5WQYQ3bV5Q/V9alVyUw8Uep15GFZ4AD2WJALdLY=", "OfVSWQYQbVQ/ValVyUwUepGFZADWJALdLY=")]
    public void ExtractNumbers(string data, string expected)
    {
        string result = Extractor.ExtractNumbers(new StringBuilder(data)).ToString();

        Assert.That(result == expected);
    }
}
