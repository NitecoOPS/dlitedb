using log4net;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Reflection;

namespace DLiteDB.StressTest
{
    public class SqlDB
    {
        private readonly string _taskName;
        private readonly DLiteDatabase _db;
        private readonly ILog _logger;
        private readonly Stopwatch _watch;
        private readonly ConcurrentCounter _concurrent;

        private long _delay;

        public int Index { get; }

        public SqlDB(string taskName, DLiteDatabase db, ILog logger, Stopwatch watch, ConcurrentCounter concurrent, int index)
        {
            _taskName = taskName;
            _db = db;
            _logger = logger;
            _watch = watch;
            _concurrent = concurrent;

            _delay = watch.ElapsedMilliseconds;

            this.Index = index;
        }

        public object Execute<T>(string methodName, string collection, params object[] args)
        {
            var log = new Log
            {
                Task = _taskName,
                Timer = (int)_watch.ElapsedMilliseconds,
                Concurrent = _concurrent.Increment() - 1,
                Delay = (int)(_watch.ElapsedMilliseconds - _delay),
                Collection = collection,
                Params = JsonConvert.SerializeObject(args)
            };

            var start = DateTime.Now;

            try
            {
                var coll = _db.GetCollection<T>(collection);
                var method = coll.FindMethod(methodName, ref args);
                if (method == null) return null;

                return method.Invoke(coll, args);
            }
            catch (Exception ex)
            {
                log.Error = ex.Message + '\n' + ex.StackTrace;

                throw ex;
            }
            finally
            {
                _concurrent.Decrement();

                log.Elapsed = DateTime.Now.Subtract(start).TotalMilliseconds;

                _logger.Info(JsonConvert.SerializeObject(log));

                _delay = _watch.ElapsedMilliseconds;
            }
        }
    }
}
