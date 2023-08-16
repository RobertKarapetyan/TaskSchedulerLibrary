using System;

namespace TaskSchedulerLibrary.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HighPriorityAttribute : Attribute { }
}
