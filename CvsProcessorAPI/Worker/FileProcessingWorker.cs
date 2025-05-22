using CsvProcessorAPI.Queue;
using CsvProcessorAPI.Services;
using CvsProcessorAPI.Services;

namespace CsvProcessorAPI.Worker
{
    public class FileProcessingWorker : BackgroundService
    {
        private readonly ILogger<FileProcessingWorker> _logger;
        private readonly IFileProcessingQueue _queue;
        private readonly ICsvValidator _validator;
        private readonly IErrorQueue _errorQueue;

        public FileProcessingWorker(
            ILogger<FileProcessingWorker> logger,
            IFileProcessingQueue queue,
            ICsvValidator validator,
            IErrorQueue errorQueue)
        {
            _logger = logger;
            _queue = queue;
            _validator = validator;
            _errorQueue = errorQueue;
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
                if (_validator.IsValidLine(line))
                {
                    _logger.LogInformation($"✅ Valid line: {line}");
                }
                else
                {
                    _logger.LogWarning($"❌ Invalid line: {line}");
                    _errorQueue.AddError(filePath, line);
                }
            }

            File.Delete(filePath); // Clean up
            _logger.LogInformation($"Finished processing and deleted: {filePath}");
        }
    }
}
