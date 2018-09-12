namespace AzureFunctionSample.Services
{
    public interface IMessageQueueService
    {
        bool AddMessageToQueue(string message);
    }
}
