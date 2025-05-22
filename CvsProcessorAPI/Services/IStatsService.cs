namespace CsvProcessorAPI.Services
{
    public interface IStatsService
    {
        void IncrementFileCount();
        int GetProcessedFileCount();
    }
}
