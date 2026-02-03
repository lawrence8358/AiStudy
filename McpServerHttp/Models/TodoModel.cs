namespace McpServerHttp.Models;

/// <summary>
/// 待辦事項資料模型
/// </summary>
public class TodoModel
{
    /// <summary>
    /// 待辦事項唯一識別碼
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 待辦事項標題
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 待辦事項描述
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 是否已完成
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// 到期日期（可為空）
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// 優先級
    /// </summary>
    public TodoPriority Priority { get; set; } = TodoPriority.Normal;

    /// <summary>
    /// 建立 TodoModel 的深層複製
    /// </summary>
    public TodoModel Clone() => new()
    {
        Id = Id,
        Title = Title,
        Description = Description,
        IsCompleted = IsCompleted,
        CreatedAt = CreatedAt,
        DueDate = DueDate,
        Priority = Priority
    };
}
