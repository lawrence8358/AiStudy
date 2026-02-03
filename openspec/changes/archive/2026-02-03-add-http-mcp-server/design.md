# Design: HTTP MCP Server 技術設計

## Context

MCP (Model Context Protocol) 支援多種傳輸協定，包括：
- **Stdio** - 透過標準輸入/輸出，適合本地 Console 應用程式
- **HTTP/SSE** - 透過 HTTP 與 Server-Sent Events，適合 Web 服務與遠端調用

現有 `McpServer` 使用 Stdio 傳輸，本專案將建立使用 HTTP/SSE 傳輸的版本。

## Goals / Non-Goals

**Goals:**
- 建立可獨立運行的 HTTP MCP Server
- 使用 ASP.NET Core Minimal API 架構
- 實作完整的待辦事項 CRUD 功能
- 提供單元測試確保程式碼品質
- 撰寫詳細的 README 文件

**Non-Goals:**
- 不實作資料持久化（使用記憶體模擬）
- 不實作使用者認證/授權
- 不實作高可用性或負載均衡

## Decisions

### 1. 傳輸協定選擇
- **選擇**: 使用 `ModelContextProtocol.AspNetCore` 套件提供的 SSE 傳輸
- **原因**: 官方推薦的 HTTP 傳輸方式，與 ASP.NET Core 整合良好

### 2. 框架選擇
- **選擇**: ASP.NET Core Minimal API
- **原因**: 輕量、快速啟動、適合 API 服務

### 3. 待辦事項功能設計
| Tool 名稱 | 功能描述 |
|-----------|----------|
| `GetAllTodos` | 取得所有待辦事項 |
| `GetTodoById` | 依 ID 取得單一待辦事項 |
| `AddTodo` | 新增待辦事項 |
| `UpdateTodo` | 更新待辦事項 |
| `DeleteTodo` | 刪除待辦事項 |
| `SearchTodos` | 搜尋待辦事項（依關鍵字） |
| `ToggleTodoStatus` | 切換待辦事項完成狀態 |

### 4. 資料模型
```csharp
public class TodoModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public string Priority { get; set; } = "Normal"; // Low, Normal, High
}
```

### 5. 套件版本
| 套件名稱 | 版本 | License |
|----------|------|---------|
| `ModelContextProtocol` | 0.6.0-preview.1 | Apache 2.0 |
| `ModelContextProtocol.AspNetCore` | 0.6.0-preview.1 | Apache 2.0 |
| `Microsoft.Extensions.Hosting` | 10.0.0 | MIT |
| `xunit` | 2.9.* | Apache 2.0 |
| `Moq` | 4.20.* | BSD-3-Clause |

## Risks / Trade-offs

- **風險**: `ModelContextProtocol` 仍在 Preview 階段，API 可能變動
  - **緩解**: 使用最新版本，並保持程式碼簡潔以便後續升級

- **風險**: 記憶體儲存在服務重啟後會遺失資料
  - **緩解**: 這是測試專案，資料持久化不在範圍內

## Open Questions

1. 預設監聽的 Port 設定為 5050（避免與其他常用 Port 衝突）
2. 是否需要 CORS 設定？（暫時開放所有來源以便測試）
