namespace CsvProcessorAPI.Services
{
    public class InMemoryStatsService : IStatsService
    {
        private int _count = 0;

        public void IncrementFileCount() => _count++;
        public int GetProcessedFileCount() => _count;
    }
}
