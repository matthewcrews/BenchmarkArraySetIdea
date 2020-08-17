
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchmarkArraySetIdea
{
    public class Program
    {



        //[Params(100, 200, 1000, 2000)]
        //public int PopulationSize
        //{
        //    get; set;
        //}

        //[Params(10, 20, 100, 200)]
        //public int NumberOfValues
        //{
        //    get; set;
        //}

        // Gen
        //[Benchmark]
        //public FSharpSet<int>[] SetGen()
        //{
        //    return KeySet.Tests.Set.genData(1000, PopulationSize, NumberOfValues);
        //}

        //[Benchmark]
        //public KeySet<int>[] KeySetGen()
        //{
        //    return KeySet.Tests.KeySet.genData(1000, PopulationSize, NumberOfValues);
        //}
        // Union
        [Benchmark]
        public int SetUnion()
        {
            return KeySet.Tests.Set.unionTest();
        }

        [Benchmark]
        public int KeySetUnion()
        {
            return KeySet.Tests.KeySet.unionTest();
        }

        // Intersect
        [Benchmark]
        public int SetIntersect()
        {
            return KeySet.Tests.Set.intersectTest();
        }

        [Benchmark]
        public int KeySetIntersect()
        {
            return KeySet.Tests.KeySet.intersectTest();
        }





        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Program>();
        }
    }
}
