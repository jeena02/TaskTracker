using FluentAssertions;
using Moq;
using NUnit.Framework;
using TaskTracker.Tests.Builders;
using TaskTracker.Application.Interfaces;
using TaskTracker.Application.Services;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Tests.Application;

[TestFixture]
public class TaskServiceTests
{
    private Mock<ITaskRepository> _repoMock;
    private TaskService _service;

    [SetUp]
    public void Setup()
    {
        _repoMock = new Mock<ITaskRepository>();
        _service = new TaskService(_repoMock.Object);
    }

    [Test]
    public async Task Create_Should_Succeed()
    {
        // Arrange
        var task = new TaskItemBuilder()
            .WithTitle("Test Task")
            .Build();

        _repoMock
            .Setup(r => r.AddAsync(It.IsAny<TaskItem>()))
            .Returns(Task.FromResult(task));

        // Act
        var result = await _service.CreateAsync(task);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Test Task");

        _repoMock.Verify(r =>
            r.AddAsync(It.IsAny<TaskItem>()),
            Times.Once);
    }    

    [Test]
    public async Task GetAll_Should_Return_Tasks()
    {
        // Arrange
        var tasks = new List<TaskItem>
        {
            new TaskItemBuilder().WithTitle("A").Build(),
            new TaskItemBuilder().WithTitle("B").Build()
        };

        _repoMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(tasks);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(t => t.Title == "A");
    }

    [Test]
    public async Task Update_Should_Succeed()
    {
        // Arrange
        var id = new Guid();
        var existing = new TaskItemBuilder()
            .WithTitle("Old")
            .Build();

        var update = new TaskItemBuilder()
            .WithTitle("New")
            .Build();

        _repoMock
            .Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(existing);

        _repoMock
            .Setup(r => r.UpdateAsync(It.IsAny<TaskItem>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.UpdateAsync(id, update);

        // Assert
        result.Should().BeTrue();
        existing.Title.Should().Be("New");

        _repoMock.Verify(r =>
            r.UpdateAsync(existing),
            Times.Once);
    }    

    [Test]
    public async Task Update_Should_Return_False_When_Not_Found()
    {
        // Arrange
        _repoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((TaskItem?)null);

        var update = new TaskItemBuilder()
            .WithTitle("X")
            .Build();

        // Act
        var result = await _service.UpdateAsync(new Guid(), update);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task Delete_Should_Return_True_When_Exists()
    {
        // Arrange
        var id = new Guid();
        var task = new TaskItemBuilder()
            .WithTitle("Delete Me")
            .Build();

        _repoMock
            .Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(task);

        _repoMock
            .Setup(r => r.DeleteAsync(task))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.DeleteAsync(id);

        // Assert
        result.Should().BeTrue();

        _repoMock.Verify(r =>
            r.DeleteAsync(task),
            Times.Once);
    }
}