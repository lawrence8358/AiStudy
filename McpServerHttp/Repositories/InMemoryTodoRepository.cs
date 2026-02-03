using McpServerHttp.Data;
using McpServerHttp.Models;

namespace McpServerHttp.Repositories;

/// <summary>
/// 待辦事項記憶體儲存庫實作 (遵循 SRP - Single Responsibility Principle)
/// </summary>
public class InMemoryTodoRepository : ITodoRepository
{
    private readonly List<TodoModel> _todos;
    private int _nextId;
    private readonly object _lock = new();

    public InMemoryTodoRepository()
    {
        _todos = TodoDataSeeder.GetInitialData();
        _nextId = _todos.Count > 0 ? _todos.Max(t => t.Id) + 1 : 1;
    }

    public IEnumerable<TodoModel> GetAll()
    {
        lock (_lock)
        {
            return _todos.Select(t => t.Clone()).ToList();
        }
    }

    public TodoModel? GetById(int id)
    {
        lock (_lock)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            return todo?.Clone();
        }
    }

    public TodoModel Add(string title, string description, TodoPriority priority = TodoPriority.Normal, DateTime? dueDate = null)
    {
        lock (_lock)
        {
            var todo = new TodoModel
            {
                Id = _nextId++,
                Title = title,
                Description = description,
                Priority = priority,
                DueDate = dueDate,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };
            _todos.Add(todo);
            return todo.Clone();
        }
    }

    public TodoModel? Update(int id, string? title = null, string? description = null, TodoPriority? priority = null, DateTime? dueDate = null)
    {
        lock (_lock)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) return null;

            if (title != null) todo.Title = title;
            if (description != null) todo.Description = description;
            if (priority != null) todo.Priority = priority.Value;
            if (dueDate != null) todo.DueDate = dueDate;

            return todo.Clone();
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) return false;
            return _todos.Remove(todo);
        }
    }

    public IEnumerable<TodoModel> Search(string keyword)
    {
        lock (_lock)
        {
            return _todos
                .Where(t => t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                           t.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .Select(t => t.Clone())
                .ToList();
        }
    }

    public TodoModel? ToggleStatus(int id)
    {
        lock (_lock)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) return null;
            todo.IsCompleted = !todo.IsCompleted;
            return todo.Clone();
        }
    }
}
