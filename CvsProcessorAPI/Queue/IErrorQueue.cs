namespace CsvProcessorAPI.Queue
{
    public interface IErrorQueue
    {
        void AddError(string fileName, string line);
        List<string> GetErrors();
    }
}
