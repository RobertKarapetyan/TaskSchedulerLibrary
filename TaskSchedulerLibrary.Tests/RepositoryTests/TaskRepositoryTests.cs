﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TaskSchedulerLibrary.Models;
using TaskSchedulerLibrary.Repository;
using TaskSchedulerLibrary.Services;
using Moq;

namespace TaskSchedulerLibrary.Tests.RepositoryTests
{
    [TestClass]
    public class TaskRepositoryTests
    {
        private TaskRepository _taskRepository;
        private Mock<NotificationService> _mockNotificationService;

        [TestInitialize]
        public void SetUp()
        {
            _mockNotificationService = new Mock<NotificationService>();
            _taskRepository = new TaskRepository(_mockNotificationService.Object);
        }

        [TestMethod]
        public void Add_SingleTask_TaskCountIncreasesByOne()
        {
            var task = new TaskItem { Name = "Test Task", DueDate = DateTime.Now.AddDays(1) };
            int initialCount = _taskRepository.GetAll().Count;

            _taskRepository.Add(task);
            int postAddCount = _taskRepository.GetAll().Count;

            Assert.AreEqual(initialCount + 1, postAddCount);
        }

        [TestMethod]
        public void Update_TaskStatus_ChangesAreReflected()
        {
            var task = new TaskItem { Name = "Update Test Task", DueDate = DateTime.Now.AddDays(2) };
            _taskRepository.Add(task);
            task.IsCompleted = true;

            _taskRepository.Update(task);

            var updatedTask = _taskRepository.GetById(task.Id);
            Assert.IsTrue(updatedTask.IsCompleted);
        }

        [TestMethod]
        public void Delete_Task_RemovesTaskFromRepository()
        {
            var task = new TaskItem { Name = "Delete Test Task", DueDate = DateTime.Now.AddDays(3) };
            _taskRepository.Add(task);

            _taskRepository.Delete(task.Id);

            var deletedTask = _taskRepository.GetById(task.Id);
            Assert.IsNull(deletedTask);
        }

        [TestMethod]
        public void Update_ExistingTask_TaskStatusChangedEventFired()
        {
            var task = new TaskItem { Id = Guid.NewGuid(), Name = "Test Task", DueDate = DateTime.Now, IsCompleted = false };
            _taskRepository.Add(task);
            bool eventWasFired = false;
            _taskRepository.TaskStatusChanged += (changedTask) => eventWasFired = true;

            task.Name = "Updated Task";
            _taskRepository.Update(task);

            Assert.IsTrue(eventWasFired);
        }

        [TestMethod]
        public void Update_NonExistingTask_TaskStatusChangedEventNotFired()
        {
            var task = new TaskItem { Id = Guid.NewGuid(), Name = "Test Task", DueDate = DateTime.Now, IsCompleted = false };
            bool eventWasFired = false;
            _taskRepository.TaskStatusChanged += (changedTask) => eventWasFired = true;

            task.Name = "Updated Task";
            _taskRepository.Update(task);

            Assert.IsFalse(eventWasFired);
        }

        [TestMethod]
        public void AddTask_SendsNotification()
        {
            var task = new TaskItem { Name = "Sample Task", DueDate = DateTime.Now.AddDays(5) };

            _mockNotificationService.Setup(service => service.SendEmailNotificationAsync(It.IsAny<TaskItem>()));

            _taskRepository.Add(task);

            _mockNotificationService.Verify(service => service.SendEmailNotificationAsync(task), Times.Once);
        }
    }
}
