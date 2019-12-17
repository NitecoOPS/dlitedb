using System.Threading;

namespace DLiteDB.StressTest
{
    public class ConcurrentCounter
    {
        private int _counter;

        public int Increment()
        {
            return Interlocked.Increment(ref _counter);
        }

        public int Decrement()
        {
            return Interlocked.Decrement(ref _counter);
        }
    }
}
