namespace DLiteDB.StressTest
{
    public class Log
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public int Timer { get; set; }
        public int Delay { get; set; }
        public double Elapsed { get; set; }
        public int Concurrent { get; set; }
        public string Collection { get; set; }
        public string Params { get; set; }
        public string Error { get; set; }
    }
}
