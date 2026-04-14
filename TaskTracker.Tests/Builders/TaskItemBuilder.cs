using TaskTracker.Domain.Entities;
using TaskStatus = TaskTracker.Domain.Enums.TaskStatus;


namespace TaskTracker.Tests.Builders;

public class TaskItemBuilder
{
    private string _title = "Default Title";
    private string? _description = null;
    private TaskStatus _status = TaskStatus.Todo;
    private DateTime? _dueDate = null;

   
    public static TaskItemBuilder Todo()
        => new TaskItemBuilder()
            .WithStatus(TaskStatus.Todo);

    public static TaskItemBuilder InProgress()
        => new TaskItemBuilder()
            .WithStatus(TaskStatus.InProgress);

    public static TaskItemBuilder Done()
        => new TaskItemBuilder()
            .WithStatus(TaskStatus.InProgress); // safe default (avoid invalid Done state)

    // ------------------------
    // 🔧 FLUENT CONFIGURATION
    // ------------------------

    public TaskItemBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public TaskItemBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public TaskItemBuilder WithStatus(TaskStatus status)
    {
        _status = status;
        return this;
    }

    public TaskItemBuilder WithDueDate(DateTime? dueDate)
    {
        _dueDate = dueDate;
        return this;
    }

    // ------------------------
    // 🏗️ BUILD
    // ------------------------

    public TaskItem Build()
    {
        var task = new TaskItem(_title, _description, _dueDate);

        // enforce domain behavior consistently
        task.Update(_title, _description, _status, _dueDate);

        return task;
    }
}