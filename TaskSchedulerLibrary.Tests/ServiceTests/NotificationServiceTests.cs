using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
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

        [TestMethod]
        public async Task SendMultipleEmailNotificationsAsync_AllNotificationsLogged()
        {
            int numberOfTasks = 100;
            var tasks = Enumerable.Range(1, numberOfTasks).Select(i =>
                new TaskItem { Name = $"Task {i}", DueDate = DateTime.Now.AddDays(i) }).ToList();

            var sendTasks = tasks.Select(task => _notificationService.SendEmailNotificationAsync(task));

            await Task.WhenAll(sendTasks);

            var notifications = _notificationService.GetAllNotifications().ToList();

            Assert.AreEqual(numberOfTasks, notifications.Count);
        }

        [DataTestMethod]
        [DataRow("Read Test Task 1")]
        [DataRow("Read Test Task 2")]
        [DataRow("Special Task !@#")]
        public async Task GetNotification_ReturnsCorrectNotification(string taskName)
        {
            var task = new TaskItem { Name = taskName, DueDate = DateTime.Now.AddDays(4) };
            await _notificationService.SendEmailNotificationAsync(task);

            var notification = _notificationService.GetNotification(taskName);

            Assert.IsTrue(notification.Contains(taskName));
        }

        [DataTestMethod]
        [DataRow("Delete Test Task 1")]
        [DataRow("Delete Test Task 2")]
        [DataRow("Unique Task $$$")]
        public async Task DeleteNotification_RemovesNotification(string taskName)
        {
            var task = new TaskItem { Name = taskName, DueDate = DateTime.Now.AddDays(4) };
            await _notificationService.SendEmailNotificationAsync(task);

            bool isDeleted = _notificationService.DeleteNotification(taskName);
            var deletedNotification = _notificationService.GetNotification(taskName);

            Assert.IsTrue(isDeleted);
            Assert.IsNull(deletedNotification);
        }
    }
}
