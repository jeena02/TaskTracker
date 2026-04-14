using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskTracker.Domain.Enums;

namespace TaskTracker.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string Title { get; private set; } = null!;

        public string? Description { get; private set; }

        public Enums.TaskStatus Status;
        public DateTime? DueDate { get; private set; }

        public TaskItem(string title, string? description, DateTime? dueDate)
        {
            SetTitle(title);
            Description = description;
            DueDate = dueDate;
            Status = Enums.TaskStatus.Todo;
        }

        public void Update(string title, string? description, Enums.TaskStatus status, DateTime? dueDate)
        {
            SetTitle(title);

            if (status == Enums.TaskStatus.Done && string.IsNullOrWhiteSpace(title))
                throw new InvalidOperationException("Task cannot be marked Done with empty title.");

            Status = status;
            Description = description;
            DueDate = dueDate;
        }

        private void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title) || title.Length > 100)
                throw new ArgumentException("Invalid title");

            Title = title;
        }

    }
}
