namespace CsvProcessorAPI.Queue
{
    public interface IFileProcessingQueue
    {
        void Enqueue(string filePath);
        bool TryDequeue(out string filePath);
    }
}
