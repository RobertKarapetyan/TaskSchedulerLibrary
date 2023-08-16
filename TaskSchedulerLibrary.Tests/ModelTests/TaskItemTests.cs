using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaskSchedulerLibrary.Models;

namespace TaskSchedulerLibrary.Tests.ModelTests
{
    [TestClass]
    public class TaskItemTests
    {
        [TestMethod]
        public void ToString_ValidTask_ReturnsFormattedString()
        {
            var task = new TaskItem
            {
                Name = "Sample Task",
                DueDate = new DateTime(2023, 8, 20)  
            };

            var expectedOutput = "Sample Task (Due: 8/20/2023)"; 

            var result = task.ToString();

            Assert.AreEqual(expectedOutput, result);
        }
    }
}
