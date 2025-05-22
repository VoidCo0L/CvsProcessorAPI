using CsvProcessorAPI.Queue;

namespace CsvProcessorAPI.Worker
{
    public class FileProcessingWorker : BackgroundService
    {
        private readonly ILogger<FileProcessingWorker> _logger;
        private readonly IFileProcessingQueue _queue;

        public FileProcessingWorker(ILogger<FileProcessingWorker> logger, IFileProcessingQueue queue)
        {
            _logger = logger;
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running...");

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_queue.TryDequeue(out var filePath))
                {
                    _logger.LogInformation($"Processing file: {filePath}");
                    await ProcessFileAsync(filePath);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task ProcessFileAsync(string filePath)
        {
            var lines = await File.ReadAllLinesAsync(filePath);
            foreach (var line in lines)
            {
                // Simulate processing line
                _logger.LogInformation($"Processed line: {line}");
            }

            File.Delete(filePath); // Clean up
            _logger.LogInformation($"Finished processing and deleted: {filePath}");
        }
    }
}
