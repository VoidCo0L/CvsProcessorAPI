using CvsProcessorAPI.Services;

namespace CsvProcessorAPI.Services
{
    public class SimpleCsvValidator : ICsvValidator
    {
        public bool IsValidLine(string line)
        {
            // Expecting 3 columns: Name, Email, Age
            var columns = line.Split(',');
            return columns.Length == 3 && int.TryParse(columns[2], out _);
        }
    }
}