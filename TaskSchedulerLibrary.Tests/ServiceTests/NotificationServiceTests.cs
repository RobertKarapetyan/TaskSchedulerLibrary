using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TaskSchedulerLibrary.Models;
using TaskSchedulerLibrary.Services;

namespace TaskSchedulerLibrary.Tests.ServiceTests
{
    [TestClass]
    public class NotificationServiceTests
    {
        private NotificationService _notificationService;

        [TestInitialize]
        public void SetUp()
        {
            _notificationService = new NotificationService();
        }

        [TestMethod]
        public async Task SendEmailNotificationAsync_DoesNotThrowException()
        {
            var task = new TaskItem { Name = "Notification Test Task", DueDate = DateTime.Now.AddDays(4) };

            try
            {
                await _notificationService.SendEmailNotificationAsync(task);
                Assert.IsTrue(true); 
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
    }
}
