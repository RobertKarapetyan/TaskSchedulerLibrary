using System;

namespace TaskSchedulerLibrary.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            return $"{Name} (Due: {DueDate.ToShortDateString()})";
        }
    }
}
