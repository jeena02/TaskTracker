using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Interfaces
{
    public interface ITaskRepository
    {
        // Method signatures for CRUD operations
        Task<TaskItem> AddAsync(TaskItem task);
        Task<List<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(TaskItem task);

    }
}
