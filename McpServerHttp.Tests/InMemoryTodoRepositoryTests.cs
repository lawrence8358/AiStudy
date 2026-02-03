using McpServerHttp.Models;
using McpServerHttp.Repositories;

namespace McpServerHttp.Tests;

/// <summary>
/// InMemoryTodoRepository 單元測試
/// </summary>
public class InMemoryTodoRepositoryTests
{
    private readonly ITodoRepository _repository;

    public InMemoryTodoRepositoryTests()
    {
        _repository = new InMemoryTodoRepository();
    }

    [Fact]
    public void GetAll_ReturnsInitialTestData()
    {
        // Arrange & Act
        var todos = _repository.GetAll().ToList();

        // Assert
        Assert.NotEmpty(todos);
        Assert.Equal(5, todos.Count);
    }

    [Fact]
    public void GetById_WithValidId_ReturnsTodo()
    {
        // Arrange
        var expectedId = 1;

        // Act
        var todo = _repository.GetById(expectedId);

        // Assert
        Assert.NotNull(todo);
        Assert.Equal(expectedId, todo.Id);
    }

    [Fact]
    public void GetById_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var invalidId = 999;

        // Act
        var todo = _repository.GetById(invalidId);

        // Assert
        Assert.Null(todo);
    }

    [Fact]
    public void GetById_ReturnsClonedObject()
    {
        // Arrange
        var todo1 = _repository.GetById(1);
        var todo2 = _repository.GetById(1);

        // Assert - 確保返回的是複製品，而非同一個物件
        Assert.NotNull(todo1);
        Assert.NotNull(todo2);
        Assert.NotSame(todo1, todo2);
        Assert.Equal(todo1.Id, todo2.Id);
    }

    [Fact]
    public void Add_CreatesTodoWithCorrectProperties()
    {
        // Arrange
        var title = "測試待辦事項";
        var description = "這是測試描述";
        var priority = TodoPriority.High;
        var dueDate = DateTime.UtcNow.AddDays(7);

        // Act
        var todo = _repository.Add(title, description, priority, dueDate);

        // Assert
        Assert.NotNull(todo);
        Assert.True(todo.Id > 0);
        Assert.Equal(title, todo.Title);
        Assert.Equal(description, todo.Description);
        Assert.Equal(priority, todo.Priority);
        Assert.Equal(dueDate, todo.DueDate);
        Assert.False(todo.IsCompleted);
    }

    [Fact]
    public void Update_WithValidId_UpdatesTodo()
    {
        // Arrange
        var newTitle = "更新後的標題";
        var existingId = 1;

        // Act
        var updated = _repository.Update(existingId, title: newTitle);

        // Assert
        Assert.NotNull(updated);
        Assert.Equal(newTitle, updated.Title);
    }

    [Fact]
    public void Update_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var invalidId = 999;

        // Act
        var result = _repository.Update(invalidId, title: "不會更新");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Update_WithPriority_UpdatesPriorityCorrectly()
    {
        // Arrange
        var existingId = 1;
        var newPriority = TodoPriority.Low;

        // Act
        var updated = _repository.Update(existingId, priority: newPriority);

        // Assert
        Assert.NotNull(updated);
        Assert.Equal(newPriority, updated.Priority);
    }

    [Fact]
    public void Delete_WithValidId_RemovesTodo()
    {
        // Arrange
        var todo = _repository.Add("要刪除的項目", "描述", TodoPriority.Normal);
        var idToDelete = todo.Id;
        var countBefore = _repository.GetAll().Count();

        // Act
        var success = _repository.Delete(idToDelete);
        var countAfter = _repository.GetAll().Count();

        // Assert
        Assert.True(success);
        Assert.Equal(countBefore - 1, countAfter);
        Assert.Null(_repository.GetById(idToDelete));
    }

    [Fact]
    public void Delete_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        var invalidId = 999;

        // Act
        var success = _repository.Delete(invalidId);

        // Assert
        Assert.False(success);
    }

    [Fact]
    public void Search_WithMatchingKeyword_ReturnsResults()
    {
        // Arrange
        var keyword = "MCP";

        // Act
        var results = _repository.Search(keyword).ToList();

        // Assert
        Assert.NotEmpty(results);
        Assert.All(results, t =>
            Assert.True(
                t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                t.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)));
    }

    [Fact]
    public void Search_WithNonMatchingKeyword_ReturnsEmpty()
    {
        // Arrange
        var keyword = "不存在的關鍵字XYZ123";

        // Act
        var results = _repository.Search(keyword).ToList();

        // Assert
        Assert.Empty(results);
    }

    [Fact]
    public void ToggleStatus_WithValidId_TogglesIsCompleted()
    {
        // Arrange
        var todo = _repository.GetById(1);
        Assert.NotNull(todo);
        var originalStatus = todo.IsCompleted;

        // Act
        var result = _repository.ToggleStatus(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(!originalStatus, result.IsCompleted);
    }

    [Fact]
    public void ToggleStatus_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var invalidId = 999;

        // Act
        var result = _repository.ToggleStatus(invalidId);

        // Assert
        Assert.Null(result);
    }
}
