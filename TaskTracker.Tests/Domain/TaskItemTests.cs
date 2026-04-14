using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.Domain.Enums;
using TaskTracker.Tests.Builders;

namespace TaskTracker.Tests.Domain
{
    [TestFixture]
    public class TaskItemTests
    {
        [Test]
        public void Update_Should_Throw_When_Marking_Done_With_Empty_Title()
        {
            // Arrange
            var task = new TaskItemBuilder()
                .WithTitle("Valid")
                .Build();

            // Act
            Action act = () => task.Update(" ", null, TaskTracker.Domain.Enums.TaskStatus.Done, null);

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Update_Should_Set_Status_To_Done_When_Valid()
        {
            // Arrange
            var task = new TaskItemBuilder().Build();

            // Act
            task.Update("Valid Title", null, TaskTracker.Domain.Enums.TaskStatus.Done, null);

            // Assert
            task.Status.Should().Be(TaskTracker.Domain.Enums.TaskStatus.Done);
        }        
    }
}
