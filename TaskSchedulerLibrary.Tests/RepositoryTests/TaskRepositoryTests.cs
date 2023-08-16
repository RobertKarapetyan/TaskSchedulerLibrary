using TaskSchedulerLibrary.Models;
using TaskSchedulerLibrary.Repository;

[TestClass]
public class TaskRepositoryTests
{
    private TaskRepository _taskRepository;

    [TestInitialize]
    public void SetUp()
    {
        _taskRepository = new TaskRepository();
    }

    [TestMethod]
    public void Add_SingleTask_TaskCountIncreasesByOne()
    {
        // Arrange
        var task = new TaskItem { Name = "Test Task", DueDate = DateTime.Now.AddDays(1) };
        int initialCount = _taskRepository.GetAll().Count;

        // Act
        _taskRepository.Add(task);
        int postAddCount = _taskRepository.GetAll().Count;

        // Assert
        Assert.AreEqual(initialCount + 1, postAddCount);
    }
}
