using BenchmarkDotNet.Running;

namespace PasswordGenerator.Benchmark;

internal class Program
{
    static void Main(string[] args) => BenchmarkRunner.Run<Benchmarks>();
}
