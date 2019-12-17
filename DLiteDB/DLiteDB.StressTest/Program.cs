using System;

namespace DLiteDB.StressTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Stress Test");
            Console.WriteLine("===========");
            Console.Write("Enter time (minutes): ");
            var timer = Console.ReadLine();

            using (var e = new ExampleStressTest())
            {
                e.Synced = true;

                e.Run(TimeSpan.FromMinutes(string.IsNullOrEmpty(timer) ? .5 : Convert.ToDouble(timer)));
            }

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }


}
