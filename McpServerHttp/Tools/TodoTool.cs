using McpServerHttp.Models;
using McpServerHttp.Repositories;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServerHttp.Tools;

/// <summary>
/// 待辦事項 MCP 工具類別 (遵循 DIP - Dependency Inversion Principle)
/// </summary>
[McpServerToolType]
public class TodoTool
{
    private readonly ITodoRepository _repository;

    public TodoTool(ITodoRepository repository)
    {
        _repository = repository;
    }

    [McpServerTool, Description("取得所有待辦事項清單")]
    public IEnumerable<TodoModel> GetAllTodos()
        => _repository.GetAll();

    [McpServerTool, Description("依 ID 取得單一待辦事項")]
    public TodoModel? GetTodoById([Description("待辦事項 ID")] int id)
        => _repository.GetById(id);

    [McpServerTool, Description("新增待辦事項")]
    public TodoModel AddTodo(
        [Description("待辦事項標題")] string title,
        [Description("待辦事項描述")] string description,
        [Description("優先級 (Low, Normal, High)，預設為 Normal")] string priority = "Normal",
        [Description("到期日期 (格式: yyyy-MM-dd)，可不填")] string? dueDateStr = null)
    {
        var priorityEnum = ParsePriority(priority);
        var dueDate = ParseDueDate(dueDateStr);
        return _repository.Add(title, description, priorityEnum, dueDate);
    }

    [McpServerTool, Description("更新待辦事項")]
    public string UpdateTodo(
        [Description("待辦事項 ID")] int id,
        [Description("新標題（不更新則留空）")] string? title = null,
        [Description("新描述（不更新則留空）")] string? description = null,
        [Description("新優先級 (Low, Normal, High)（不更新則留空）")] string? priority = null,
        [Description("新到期日期 (格式: yyyy-MM-dd)（不更新則留空）")] string? dueDateStr = null)
    {
        TodoPriority? priorityEnum = string.IsNullOrEmpty(priority) ? null : ParsePriority(priority);
        var dueDate = ParseDueDate(dueDateStr);
        var result = _repository.Update(id, title, description, priorityEnum, dueDate);
        return result != null
            ? $"待辦事項 ID {id} 已更新：{result.Title}"
            : $"找不到 ID 為 {id} 的待辦事項";
    }

    [McpServerTool, Description("刪除待辦事項")]
    public string DeleteTodo([Description("待辦事項 ID")] int id)
    {
        var success = _repository.Delete(id);
        return success
            ? $"待辦事項 ID {id} 已刪除"
            : $"找不到 ID 為 {id} 的待辦事項";
    }

    [McpServerTool, Description("搜尋待辦事項（依標題或描述關鍵字）")]
    public IEnumerable<TodoModel> SearchTodos([Description("搜尋關鍵字")] string keyword)
        => _repository.Search(keyword);

    [McpServerTool, Description("切換待辦事項的完成狀態")]
    public string ToggleTodoStatus([Description("待辦事項 ID")] int id)
    {
        var result = _repository.ToggleStatus(id);
        if (result == null)
            return $"找不到 ID 為 {id} 的待辦事項";

        return result.IsCompleted
            ? $"待辦事項「{result.Title}」已標記為完成"
            : $"待辦事項「{result.Title}」已標記為未完成";
    }

    private static DateTime? ParseDueDate(string? dueDateStr)
        => DateTime.TryParse(dueDateStr, out var parsed) ? parsed : null;

    private static TodoPriority ParsePriority(string priority)
        => Enum.TryParse<TodoPriority>(priority, ignoreCase: true, out var result)
            ? result
            : TodoPriority.Normal;
}
