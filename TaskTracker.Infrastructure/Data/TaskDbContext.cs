using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Infrastructure.Data
{
    public class TaskDbContext: DbContext
    {
        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options) { }
    }
}
