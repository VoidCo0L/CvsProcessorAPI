namespace CsvProcessorAPI.Queue
{
    public class InMemoryErrorQueue : IErrorQueue
    {
        private readonly List<string> _errors = new();

        public void AddError(string fileName, string line)
        {
            _errors.Add($"[File: {fileName}] {line}");
        }

        public List<string> GetErrors() => _errors;
    }
}
