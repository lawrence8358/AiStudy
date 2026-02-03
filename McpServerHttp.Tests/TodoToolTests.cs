using McpServerHttp.Models;
using McpServerHttp.Repositories;
using McpServerHttp.Tools;

namespace McpServerHttp.Tests;

/// <summary>
/// TodoTool 單元測試
/// </summary>
public class TodoToolTests
{
    private readonly TodoTool _tool;
    private readonly ITodoRepository _repository;

    public TodoToolTests()
    {
        _repository = new InMemoryTodoRepository();
        _tool = new TodoTool(_repository);
    }

    [Fact]
    public void GetAllTodos_ReturnsAllItems()
    {
        // Act
        var todos = _tool.GetAllTodos().ToList();

        // Assert
        Assert.NotEmpty(todos);
        Assert.Equal(5, todos.Count);
    }

    [Fact]
    public void GetTodoById_WithValidId_ReturnsTodo()
    {
        // Act
        var todo = _tool.GetTodoById(1);

        // Assert
        Assert.NotNull(todo);
        Assert.Equal(1, todo.Id);
    }

    [Fact]
    public void GetTodoById_WithInvalidId_ReturnsNull()
    {
        // Act
        var todo = _tool.GetTodoById(999);

        // Assert
        Assert.Null(todo);
    }

    [Fact]
    public void AddTodo_CreatesNewTodo()
    {
        // Arrange
        var countBefore = _tool.GetAllTodos().Count();

        // Act
        var newTodo = _tool.AddTodo("新待辦", "描述內容", "High", "2025-12-31");

        // Assert
        Assert.NotNull(newTodo);
        Assert.Equal("新待辦", newTodo.Title);
        Assert.Equal(TodoPriority.High, newTodo.Priority);
        Assert.Equal(countBefore + 1, _tool.GetAllTodos().Count());
    }

    [Fact]
    public void AddTodo_WithInvalidDate_SetsNullDueDate()
    {
        // Act
        var newTodo = _tool.AddTodo("測試", "描述", "Normal", "invalid-date");

        // Assert
        Assert.NotNull(newTodo);
        Assert.Null(newTodo.DueDate);
    }

    [Fact]
    public void AddTodo_WithInvalidPriority_DefaultsToNormal()
    {
        // Act
        var newTodo = _tool.AddTodo("測試", "描述", "InvalidPriority");

        // Assert
        Assert.NotNull(newTodo);
        Assert.Equal(TodoPriority.Normal, newTodo.Priority);
    }

    [Fact]
    public void UpdateTodo_WithValidId_ReturnsSuccessMessage()
    {
        // Act
        var result = _tool.UpdateTodo(1, title: "更新後的標題");

        // Assert
        Assert.Contains("已更新", result);
        Assert.Contains("更新後的標題", result);
    }

    [Fact]
    public void UpdateTodo_WithInvalidId_ReturnsNotFoundMessage()
    {
        // Act
        var result = _tool.UpdateTodo(999, title: "不存在");

        // Assert
        Assert.Contains("找不到", result);
    }

    [Fact]
    public void DeleteTodo_WithValidId_ReturnsSuccessMessage()
    {
        // Arrange
        var newTodo = _tool.AddTodo("要刪除", "描述", "Low");

        // Act
        var result = _tool.DeleteTodo(newTodo.Id);

        // Assert
        Assert.Contains("已刪除", result);
    }

    [Fact]
    public void DeleteTodo_WithInvalidId_ReturnsNotFoundMessage()
    {
        // Act
        var result = _tool.DeleteTodo(999);

        // Assert
        Assert.Contains("找不到", result);
    }

    [Fact]
    public void SearchTodos_WithKeyword_ReturnsMatches()
    {
        // Act
        var results = _tool.SearchTodos("MCP").ToList();

        // Assert
        Assert.NotEmpty(results);
    }

    [Fact]
    public void ToggleTodoStatus_WithValidId_TogglesAndReturnsMessage()
    {
        // Arrange
        var todo = _tool.GetTodoById(1);
        Assert.NotNull(todo);
        var wasCompleted = todo.IsCompleted;

        // Act
        var result = _tool.ToggleTodoStatus(1);

        // Assert
        if (wasCompleted)
            Assert.Contains("未完成", result);
        else
            Assert.Contains("完成", result);
    }

    [Fact]
    public void ToggleTodoStatus_WithInvalidId_ReturnsNotFoundMessage()
    {
        // Act
        var result = _tool.ToggleTodoStatus(999);

        // Assert
        Assert.Contains("找不到", result);
    }
}
