using McpServerHttp.Models;

namespace McpServerHttp.Data;

/// <summary>
/// 待辦事項初始資料種子類別
/// </summary>
public static class TodoDataSeeder
{
    /// <summary>
    /// 取得初始測試資料
    /// </summary>
    public static List<TodoModel> GetInitialData()
    {
        return new List<TodoModel>
        {
            new()
            {
                Id = 1,
                Title = "完成 MCP Server HTTP 專案",
                Description = "建立使用 HTTP/SSE 傳輸的 MCP Server，實作待辦事項管理功能",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                DueDate = DateTime.UtcNow.AddDays(7),
                Priority = TodoPriority.High
            },
            new()
            {
                Id = 2,
                Title = "撰寫單元測試",
                Description = "為 TodoTool 的所有方法撰寫完整的單元測試",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                DueDate = DateTime.UtcNow.AddDays(3),
                Priority = TodoPriority.High
            },
            new()
            {
                Id = 3,
                Title = "更新專案文件",
                Description = "撰寫 README.md 並更新根目錄的 Readme.md",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                DueDate = DateTime.UtcNow.AddDays(5),
                Priority = TodoPriority.Normal
            },
            new()
            {
                Id = 4,
                Title = "學習 MCP 協定規範",
                Description = "閱讀 MCP 官方文件，了解 Tools、Prompts、Resources 等概念",
                IsCompleted = true,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                DueDate = DateTime.UtcNow.AddDays(-3),
                Priority = TodoPriority.Normal
            },
            new()
            {
                Id = 5,
                Title = "購買咖啡豆",
                Description = "去超市購買中深焙咖啡豆，偏好衣索比亞產區",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(1),
                Priority = TodoPriority.Low
            }
        };
    }
}
