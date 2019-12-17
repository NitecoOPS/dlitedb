using System;

namespace DLiteDB.StressTest
{
    public class TaskAttribute : Attribute
    {
        /// <summary>
        /// Waiting time (in milliseconds) before first run
        /// </summary>
        public int Start { get; set; } = 2000;

        /// <summary>
        /// Repeat this method every N milliseconds
        /// </summary>
        public int Repeat { get; set; } = 1000;

        /// <summary>
        /// Random time (0-N ms) for initial/repeat tasks
        /// </summary>
        public int Random { get; set; } = 500;

        /// <summary>
        /// Define how many concurrent tasks will be created
        /// </summary>
        public int Threads { get; set; } = 1;
    }
}
