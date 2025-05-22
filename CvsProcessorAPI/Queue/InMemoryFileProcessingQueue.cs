using System.Collections.Concurrent;

namespace CsvProcessorAPI.Queue
{
    public class InMemoryFileProcessingQueue : IFileProcessingQueue
    {
        private readonly ConcurrentQueue<string> _queue = new();

        public void Enqueue(string filePath)
        {
            _queue.Enqueue(filePath);
        }

        public bool TryDequeue(out string filePath)
        {
            return _queue.TryDequeue(out filePath);
        }
    }
}
