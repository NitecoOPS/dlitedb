using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DLiteDB.StressTest
{
    public abstract class StressTest : IDisposable
    {
        // "fixed" random number order
        protected readonly Random rnd = new Random(0);

        private readonly DLiteDatabase _db;

        protected static readonly ILog _logger = LogManager.GetLogger(typeof(StressTest));

        public bool Synced { get; set; } = false;

        public StressTest()
        {
            _db = DLiteDatabase.Instance;
        }

        public abstract void OnInit(SqlDB db);

        /// <summary>
        /// Run all methods
        /// </summary>
        public void Run(TimeSpan timer)
        {
            var running = true;
            var finish = DateTime.Now.Add(timer);
            var watch = new Stopwatch();
            var concurrent = new ConcurrentCounter();
            var exec = 0;

            Console.WriteLine("Start running: " + this.GetType().Name);

            // initialize database
            this.OnInit(new SqlDB("OnInit", _db, _logger, watch, concurrent, 0));

            var tasks = new List<Task>();
            var methods = this.GetType()
                .GetMethods()
                .Where(x => x.GetCustomAttribute<TaskAttribute>() != null)
                .Select(x => new Tuple<MethodInfo, TaskAttribute>(x, x.GetCustomAttribute<TaskAttribute>()))
                .ToArray();

            watch.Start();

            foreach (var method in methods)
            {
                for (var i = 0; i < method.Item2.Threads; i++)
                {
                    var index = i;

                    // create one task per method
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        var count = 0;

                        // running loop
                        while (running && DateTime.Now < finish)
                        {
                            var wait = count == 0 ? method.Item2.Start : method.Item2.Repeat;
                            var delay = wait + rnd.Next(0, method.Item2.Random);
                            var name = method.Item2.Threads == 1 ? method.Item1.Name : method.Item1.Name + "_" + index;

                            var sql = new SqlDB(name, _db, _logger, watch, concurrent, index);

                            Task.Delay(delay).GetAwaiter().GetResult();

                            try
                            {
                                if (this.Synced) Monitor.Enter(_db);

                                method.Item1.Invoke(this, new object[] { sql });

                                exec++;
                            }
                            catch (TargetInvocationException ex)
                            {
                                running = false;

                                Console.WriteLine($"ERROR {method.Item1.Name} : {ex.InnerException.Message}");
                            }
                            catch (Exception ex)
                            {
                                running = false;

                                Console.WriteLine($"ERROR {method.Item1.Name} : {ex.Message}");
                            }
                            finally
                            {
                                if (this.Synced) Monitor.Exit(_db);
                            }

                            count++;
                        }
                    }));
                }
            }

            // progress task
            tasks.Add(Task.Factory.StartNew(() =>
            {
                while (running && DateTime.Now < finish)
                {
                    Task.Delay(10000).GetAwaiter().GetResult();

                    Console.WriteLine(string.Format("{0:00}:{1:00}:{2:00}: {3}",
                        watch.Elapsed.Hours,
                        watch.Elapsed.Minutes,
                        watch.Elapsed.Seconds,
                        exec));
                }
            }));

            // wait finish all tasks
            Task.WaitAll(tasks.ToArray());
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
