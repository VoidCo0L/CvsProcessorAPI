using Microsoft.Extensions.Hosting;

namespace CsvProcessorAPI.Worker
{
    public class OldFileCleanupWorker : BackgroundService
    {
        private readonly ILogger<OldFileCleanupWorker> _logger;
        private readonly string _tempFolder = Path.GetTempPath();
        private readonly TimeSpan _cleanupInterval = TimeSpan.FromHours(6);

        public OldFileCleanupWorker(ILogger<OldFileCleanupWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                CleanupOldFiles();
                await Task.Delay(_cleanupInterval, stoppingToken);
            }
        }

        private void CleanupOldFiles()
        {
            var now = DateTime.Now;
            var files = Directory.GetFiles(_tempFolder);
            foreach (var file in files)
            {
                var creationTime = File.GetCreationTime(file);
                if ((now - creationTime).TotalHours > 24)
                {
                    try
                    {
                        File.Delete(file);
                        _logger.LogInformation($"Deleted old file: {file}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Failed to delete {file}: {ex.Message}");
                    }
                }
            }
        }
    }
}
