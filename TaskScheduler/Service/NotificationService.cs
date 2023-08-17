using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerLibrary.Models;

namespace TaskSchedulerLibrary.Services
{
    public class NotificationService
    {
        private readonly ConcurrentBag<NotificationLog> _notificationLogs = new ConcurrentBag<NotificationLog>();

        public virtual async Task SendEmailNotificationAsync(TaskItem task)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));  

            _notificationLogs.Add(new NotificationLog
            {
                TaskName = task.Name,
                SentDate = DateTime.UtcNow
            });
        }

        public IEnumerable<string> GetAllNotifications() => _notificationLogs.Select(n => n.ToString());

        public string GetNotification(string taskName)
        {
            var notification = _notificationLogs.FirstOrDefault(n => n.TaskName.Equals(taskName, StringComparison.OrdinalIgnoreCase));
            return notification?.ToString();
        }

        public bool DeleteNotification(string taskName)
        {
            var notification = _notificationLogs.FirstOrDefault(n => n.TaskName.Equals(taskName, StringComparison.OrdinalIgnoreCase));
            if (notification != null)
            {
                return _notificationLogs.TryTake(out notification);
            }
            return false;
        }

        private class NotificationLog
        {
            public string TaskName { get; set; }
            public DateTime SentDate { get; set; }

            public override string ToString()
            {
                return $"Notification for '{TaskName}' sent at {SentDate}";
            }
        }
    }
}
