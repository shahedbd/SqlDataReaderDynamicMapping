using DynamicMapping.Implementations;
using System;
using System.Diagnostics;

namespace DynamicMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started....");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Reset();
            stopWatch.Stop();


            stopWatch.Reset();

            stopWatch.Start();
            var result = new RepositoryPersonalInfo().GetAll();

            stopWatch.Stop();
            Console.WriteLine("Total execution time by manual mapping: " + stopWatch.Elapsed.TotalSeconds);




            stopWatch.Reset();

            stopWatch.Start();
            var result2 = new RepositoryPersonalInfo().GetAllDynamic();

            stopWatch.Stop();
            Console.WriteLine("Total execution time by dynamic mapping: " + stopWatch.Elapsed.TotalSeconds);


            stopWatch.Reset();

            stopWatch.Start();
            var result3 = new RepositoryPersonalInfo().GetAllDynamic2();

            stopWatch.Stop();
            Console.WriteLine("Total execution time by dynamic 2 mapping: " + stopWatch.Elapsed.TotalSeconds);



            Console.ReadKey();
        }
    }
}
