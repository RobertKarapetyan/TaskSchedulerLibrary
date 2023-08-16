using System;
using System.Collections.Generic;
using System.Linq;
using TaskSchedulerLibrary.Models;

namespace TaskSchedulerLibrary.Repository
{
    public class TaskRepository : IRepository<TaskItem>
    {
        private readonly List<TaskItem> tasks = new List<TaskItem>();

        public delegate void TaskStatusChangedDelegate(TaskItem task);
        public event TaskStatusChangedDelegate TaskStatusChanged;

        protected virtual void OnTaskStatusChanged(TaskItem task)
        {
            TaskStatusChanged?.Invoke(task);
        }

        public List<TaskItem> GetAll()
        {
            return tasks;
        }

        public TaskItem GetById(Guid id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public void Add(TaskItem entity)
        {
            tasks.Add(entity);
        }

        public void Update(TaskItem entity)
        {
            var task = tasks.FirstOrDefault(t => t.Id == entity.Id);
            if (task != null)
            {
                task.Name = entity.Name;
                task.DueDate = entity.DueDate;
                task.IsCompleted = entity.IsCompleted;
                OnTaskStatusChanged(task);
            }
        }

        public void Delete(Guid id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
            }
        }
    }
}
