namespace CvsProcessorAPI.Services
{
    public interface ICsvValidator
    {
        bool IsValidLine(string line);
    }
}
