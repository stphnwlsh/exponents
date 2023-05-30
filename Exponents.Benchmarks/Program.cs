using BenchmarkDotNet.Running;
using Exponents.Benchmarks;

Console.WriteLine("Started Running");

_ = BenchmarkRunner.Run<Benchmark>();

Console.WriteLine("Finished Running");

Console.ReadKey();
