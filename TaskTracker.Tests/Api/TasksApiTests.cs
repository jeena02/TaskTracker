using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using TaskTracker.Domain.Entities;
using TaskTracker.Tests.Api;

namespace TaskTracker.Tests;

[TestFixture]
public class TaskApiTests
{
    private HttpClient _client;
    private CustomWebFactory _factory;

    [SetUp]
    public void Setup()
    {
         _factory = new CustomWebFactory();
         _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task POST_tasks_Should_Create_Task()
    {
        var task = new TaskItem("Integration Test", null, null);

        var response = await _client.PostAsJsonAsync("/task", task);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var created = await response.Content.ReadFromJsonAsync<TaskItem>();
        created.Should().NotBeNull();
        created!.Title.Should().Be("Integration Test");
    }

    [Test]
    public async Task GET_tasks_Should_Return_List()
    {
        await _client.PostAsJsonAsync("/task", new TaskItem("Task1", null, null));

        var response = await _client.GetAsync("/task");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var tasks = await response.Content.ReadFromJsonAsync<List<TaskItem>>();
        tasks.Should().NotBeEmpty();
    }

    [Test]
    public async Task GET_tasks_id_Should_Return_404_When_NotFound()
    {
        var response = await _client.GetAsync($"/task/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task PUT_tasks_Should_Update_Task()
    {
        var createResponse = await _client.PostAsJsonAsync("/task",
            new TaskItem("Old", null, null));

        var created = await createResponse.Content.ReadFromJsonAsync<TaskItem>();

        var updated = new TaskItem("Updated", null, null);

        var response = await _client.PutAsJsonAsync($"/task/{created!.Id}", updated);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var get = await _client.GetFromJsonAsync<TaskItem>($"/task/{created.Id}");
        get!.Title.Should().Be("Updated");
    }

    [Test]
    public async Task DELETE_tasks_Should_Delete_Task()
    {
        var createResponse = await _client.PostAsJsonAsync("/task",
            new TaskItem("ToDelete", null, null));

        var created = await createResponse.Content.ReadFromJsonAsync<TaskItem>();

        var deleteResponse = await _client.DeleteAsync($"/task/{created!.Id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await _client.GetAsync($"/task/{created.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task POST_tasks_Should_Return_400_When_Invalid()
    {
        var task = new TaskItem("", null, null); // invalid title

        var response = await _client.PostAsJsonAsync("/task", task);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task PUT_tasks_Should_Enforce_Business_Rule()
    {
        var createResponse = await _client.PostAsJsonAsync("/task",
            new TaskItem("Valid", null, null));

        var created = await createResponse.Content.ReadFromJsonAsync<TaskItem>();

        var invalid = new TaskItem(" ", null, null);

        invalid = typeof(TaskItem)
            .GetConstructor(new[] { typeof(string), typeof(string), typeof(DateTime?) })!
            .Invoke(new object[] { " ", null, null }) as TaskItem;

        var response = await _client.PutAsJsonAsync(
            $"/task/{created!.Id}",
            new TaskItem(" ", null, null) // will trigger validation
        );

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}