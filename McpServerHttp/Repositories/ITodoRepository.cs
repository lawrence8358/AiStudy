using McpServerHttp.Models;

namespace McpServerHttp.Repositories;

/// <summary>
/// 待辦事項儲存庫介面 (遵循 ISP - Interface Segregation Principle)
/// </summary>
public interface ITodoRepository
{
    /// <summary>
    /// 取得所有待辦事項
    /// </summary>
    IEnumerable<TodoModel> GetAll();

    /// <summary>
    /// 依 ID 取得待辦事項
    /// </summary>
    TodoModel? GetById(int id);

    /// <summary>
    /// 新增待辦事項
    /// </summary>
    TodoModel Add(string title, string description, TodoPriority priority = TodoPriority.Normal, DateTime? dueDate = null);

    /// <summary>
    /// 更新待辦事項
    /// </summary>
    TodoModel? Update(int id, string? title = null, string? description = null, TodoPriority? priority = null, DateTime? dueDate = null);

    /// <summary>
    /// 刪除待辦事項
    /// </summary>
    bool Delete(int id);

    /// <summary>
    /// 搜尋待辦事項（依關鍵字）
    /// </summary>
    IEnumerable<TodoModel> Search(string keyword);

    /// <summary>
    /// 切換待辦事項完成狀態
    /// </summary>
    TodoModel? ToggleStatus(int id);
}
