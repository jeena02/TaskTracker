using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.Application.Interfaces;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            var newTask = new TaskItem(task.Title, task.Description, task.DueDate);

            await _taskRepository.AddAsync(newTask);
            return newTask;
        }

        public Task<List<TaskItem>> GetAllAsync()
            => _taskRepository.GetAllAsync();

        public Task<TaskItem?> GetByIdAsync(Guid id)
            => _taskRepository.GetByIdAsync(id);

        public async Task<bool> UpdateAsync(Guid id, TaskItem updated)
        {
            var existing = await _taskRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Update(
                updated.Title,
                updated.Description,
                updated.Status,
                updated.DueDate
            );

            await _taskRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return false;

            await _taskRepository.DeleteAsync(task);
            return true;
        }
    }
}
