using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Parameters;
using BenchmarkDotNet.Running;
using Perfolizer.Horology;
using static System.Console;

#if DEBUG
BenchmarkSwitcher.FromAssembly(typeof(Benchmarked.ToBenchmark).Assembly).Run(args, new DebugInProcessConfig());
#endif
var type = typeof(Benchmarked.ToBenchmark);
var job =  Job.Dry
    .With(Platform.X64)
    .With(Jit.RyuJit)
    .With(CoreRuntime.Core50)
    .WithLaunchCount(1)
    .WithIterationCount(10)
    .WithMaxIterationCount(10)
    .WithId("MySuperJob");

var info = new BenchmarkRunInfo(
    new[]
    {
        BenchmarkCase.Create(
            new Descriptor(type, type.GetMethod(nameof(Benchmarked.ToBenchmark.Bootstrap))),
            job,
            null,
            null)
    },
    type,
    null);

var summary =  BenchmarkRunner.Run(info);

WriteLine(summary.ToString());

namespace Benchmarked
{
    [SimpleJob(launchCount: 1, warmupCount: 0, targetCount: 10)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class ToBenchmark
    {
        const string _filename = "../../../../../../../../../test-data/unencrypted/test.txt";

        public static string FN { get; set; } = _filename;

        [Benchmark]
        public void Bootstrap()
        {
            ReadFile(FN, out var output);
            //WriteLine($"Length: {output.Length}, First: {output[0]}, Last: {output[output.Length-1]}");
        }

        private void ReadFile(string filename, out byte[] output)
        {
            using var stream = File.OpenRead(filename);
            output = new byte[stream.Length];
            stream.Read(output, 0, (int) stream.Length);
        }
    }
}
