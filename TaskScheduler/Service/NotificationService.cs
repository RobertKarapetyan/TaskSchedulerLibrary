using System;
using System.Threading.Tasks;
using TaskSchedulerLibrary.Models;

namespace TaskSchedulerLibrary.Services
{
    public class NotificationService
    {
        public async Task SendEmailNotificationAsync(TaskItem task)
        {
            // Simulated email sending delay.
            await Task.Delay(2000);

            // This is just a mock. In a real-world scenario, use an email sending service here.
            Console.WriteLine($"Email sent regarding task: {task.Name}");
        }
    }
}
