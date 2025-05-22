using Microsoft.AspNetCore.Mvc;
using CsvProcessorAPI.Services;
using CsvProcessorAPI.Queue;

namespace CsvProcessorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly IStatsService _stats;
        private readonly IErrorQueue _errorQueue;

        public StatsController(IStatsService stats, IErrorQueue errorQueue)
        {
            _stats = stats;
            _errorQueue = errorQueue;
        }

        [HttpGet]
        public IActionResult GetStats()
        {
            return Ok(new
            {
                FilesProcessed = _stats.GetProcessedFileCount(),
                ErrorLines = _errorQueue.GetErrors()
            });
        }
    }
}
