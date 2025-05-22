using Microsoft.AspNetCore.Mvc;
using CsvProcessorAPI.Queue;
using Microsoft.AspNetCore.Authorization;

namespace CsvProcessorAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileProcessingQueue _queue;

        public FileUploadController(IFileProcessingQueue queue)
        {
            _queue = queue;
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Save to temp folder
            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }

            _queue.Enqueue(filePath); // Add to processing queue
            return Ok("File received and queued for processing.");
        }

        [HttpPost("multiple")]
        public IActionResult UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No files uploaded.");

            foreach (var file in files)
            {
                var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                _queue.Enqueue(filePath);
            }

            return Ok($"{files.Count} file(s) received and queued for processing.");
        }

    }
}
