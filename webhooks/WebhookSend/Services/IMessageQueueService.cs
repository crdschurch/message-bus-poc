using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebhookSend.Services
{
    public interface IMessageQueueService
    {
        bool SendMessage(string message);
    }
}
