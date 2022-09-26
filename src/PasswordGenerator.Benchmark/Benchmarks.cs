using BenchmarkDotNet.Attributes;

namespace PasswordGenerator.Benchmark;

[MemoryDiagnoser(false)]
public class Benchmarks
{
    [Params(10_000, 100_000, 1_000_000, 10_000_000)]
    public int Length { get; set; }

    [Benchmark]
    public string GeneratePassword() => Core.Generator.GeneratePassword(Length, true, true);
}
