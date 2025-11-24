using Application.Dtos;
using Application.Tests.Fakes;
using Application.UseCases.Todos;
using NUnit.Framework;

namespace Application.Tests;

[TestFixture]
public class CreateTodoHandlerTests
{
    [Test]
    public async Task HandleAsync_Should_Create_Todo_With_Title_And_Not_Completed()
    {
        // Arrange
        var fakeRepo = new FakeTodoRepository();
        var handler = new CreateTodoHandler(fakeRepo);
        var command = new CreateTodoCommand
        {
            Title = "Test todo"
        };

        // Act
        TodoItemDto result = await handler.HandleAsync(command);

        // Assert
        Assert.That(result.Id, Is.GreaterThan(0));
        Assert.That(result.Title, Is.EqualTo("Test todo"));
        Assert.That(result.IsCompleted, Is.False);
    }
}
