using Kinetix.Business.Abstract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kinetix.Business.Concrete
{
    public class NotificationManager : INotificationManager
    {
        private readonly ILogger<NotificationManager> _logger;


        public NotificationManager(ILogger<NotificationManager> logger)
        {
            _logger = logger;
        }

        public void SendNotificationManager(string message,string issue)
        {
            _logger.LogWarning(message,issue);
        }
    }
}
