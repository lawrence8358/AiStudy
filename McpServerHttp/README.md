# 🔌 MCP Server HTTP - 待辦事項管理工具

這是一個使用 .NET 10 開發的 MCP (Model Context Protocol) Server 範例專案，使用 **HTTP/SSE** 傳輸協定，模擬待辦事項管理功能，讓 AI Client（如 GitHub Copilot、Claude Desktop）能夠透過 HTTP 連線調用工具。

> 📖 **相關專案**：[McpServer](../McpServer/README.md) - 使用 Stdio 傳輸的版本（員工與工作經歷查詢）

---

## 📚 專案說明

### Stdio vs HTTP/SSE 傳輸

| 特性 | Stdio | HTTP/SSE |
|------|-------|----------|
| 連線方式 | 標準輸入/輸出 | HTTP 請求 + SSE 串流 |
| 適用場景 | 本地 Console 整合 | Web 服務、遠端調用 |
| 多客戶端 | 單一客戶端 | 可支援多客戶端 |
| 範例專案 | `McpServer/` | `McpServerHttp/` (本專案) |

### 功能特色

本專案提供完整的待辦事項 CRUD 操作：

| Tool 名稱 | 功能描述 |
|-----------|----------|
| `GetAllTodos` | 取得所有待辦事項清單 |
| `GetTodoById` | 依 ID 取得單一待辦事項 |
| `AddTodo` | 新增待辦事項 |
| `UpdateTodo` | 更新待辦事項 |
| `DeleteTodo` | 刪除待辦事項 |
| `SearchTodos` | 搜尋待辦事項（依關鍵字） |
| `ToggleTodoStatus` | 切換待辦事項完成狀態 |

---

## 🛠️ 開發語言與技術棧

### 開發環境

- **.NET 10**
- **C# 13**
- **ASP.NET Core Minimal API**

### 使用套件

| 套件名稱 | 版本 | License | 用途 |
|----------|------|---------|------|
| `ModelContextProtocol` | 0.6.0-preview.1 | Apache 2.0 | MCP Server 核心套件 |
| `ModelContextProtocol.AspNetCore` | 0.6.0-preview.1 | Apache 2.0 | ASP.NET Core 整合 |

```xml
<PackageReference Include="ModelContextProtocol" Version="0.6.0-preview.1" />
<PackageReference Include="ModelContextProtocol.AspNetCore" Version="0.6.0-preview.1" />
```

> ⚠️ **注意**：所有套件皆使用允許商用的開源授權（Apache 2.0）

---

## 🏗️ 架構設計

本專案遵循 **Clean Code** 與 **SOLID** 原則：

```
McpServerHttp/
├── Models/
│   └── TodoModel.cs           # 資料模型
├── Repositories/
│   ├── ITodoRepository.cs     # 介面定義 (ISP)
│   └── InMemoryTodoRepository.cs  # 記憶體實作 (SRP)
├── Tools/
│   └── TodoTool.cs            # MCP 工具 (DIP)
├── Properties/
│   └── launchSettings.json    # 啟動設定
└── Program.cs                 # 進入點
```

### SOLID 原則應用

- **S**ingle Responsibility: `InMemoryTodoRepository` 只負責資料存取
- **O**pen/Closed: 透過介面可輕鬆替換為其他儲存實作（如 EF Core）
- **L**iskov Substitution: 任何 `ITodoRepository` 實作皆可替換
- **I**nterface Segregation: `ITodoRepository` 只定義必要的方法
- **D**ependency Inversion: `TodoTool` 依賴抽象介面而非具體實作

---

## 🔧 MCP Server 架構說明

### Program.cs - 伺服器進入點

```csharp
var builder = WebApplication.CreateBuilder(args);

// 註冊 Repository (Singleton 確保資料一致性)
builder.Services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();

// 註冊 TodoTool (由 DI 容器管理)
builder.Services.AddScoped<TodoTool>();

// 註冊 MCP Server 服務
builder.Services
    .AddMcpServer()
    .WithHttpTransport()  // 使用 HTTP/SSE 傳輸
    .WithTools<TodoTool>();

var app = builder.Build();
app.MapMcp();  // 設定 MCP SSE 端點
app.Run();
```

### TodoTool - MCP 工具定義

```csharp
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

    // ... 其他工具方法
}
```

---

## 📋 開放的 MCP 工具

### 1. GetAllTodos - 取得所有待辦事項

```csharp
[McpServerTool, Description("取得所有待辦事項清單")]
public IEnumerable<TodoModel> GetAllTodos()
```

回傳包含 `id`、`title`、`description`、`isCompleted`、`createdAt`、`dueDate`、`priority` 的待辦事項物件陣列。

### 2. GetTodoById - 依 ID 取得

```csharp
[McpServerTool, Description("依 ID 取得單一待辦事項")]
public TodoModel? GetTodoById([Description("待辦事項 ID")] int id)
```

### 3. AddTodo - 新增待辦事項

```csharp
[McpServerTool, Description("新增待辦事項")]
public TodoModel AddTodo(
    [Description("待辦事項標題")] string title,
    [Description("待辦事項描述")] string description,
    [Description("優先級 (Low, Normal, High)")] string priority = "Normal",
    [Description("到期日期 (格式: yyyy-MM-dd)")] string? dueDateStr = null)
```

### 4. UpdateTodo - 更新待辦事項

```csharp
[McpServerTool, Description("更新待辦事項")]
public string UpdateTodo(
    [Description("待辦事項 ID")] int id,
    [Description("新標題")] string? title = null,
    [Description("新描述")] string? description = null,
    [Description("新優先級")] string? priority = null,
    [Description("新到期日期")] string? dueDateStr = null)
```

### 5. DeleteTodo - 刪除待辦事項

```csharp
[McpServerTool, Description("刪除待辦事項")]
public string DeleteTodo([Description("待辦事項 ID")] int id)
```

### 6. SearchTodos - 搜尋待辦事項

```csharp
[McpServerTool, Description("搜尋待辦事項（依標題或描述關鍵字）")]
public IEnumerable<TodoModel> SearchTodos([Description("搜尋關鍵字")] string keyword)
```

### 7. ToggleTodoStatus - 切換完成狀態

```csharp
[McpServerTool, Description("切換待辦事項的完成狀態")]
public string ToggleTodoStatus([Description("待辦事項 ID")] int id)
```

---

## 🚀 啟動與測試

### 啟動伺服器

```bash
cd McpServerHttp
dotnet run
```

啟動後會顯示：
```
🚀 MCP Server HTTP 已啟動
📍 MCP 端點: http://localhost:5050/sse
📋 可用工具: GetAllTodos, GetTodoById, AddTodo, UpdateTodo, DeleteTodo, SearchTodos, ToggleTodoStatus
```

### 驗證伺服器運行

```bash
# 健康檢查
curl http://localhost:5050/health

# 回應
{"status":"healthy","timestamp":"2025-01-22T00:30:00Z"}
```

### 執行單元測試

```bash
cd McpServerHttp.Tests
dotnet test
```

預期輸出：
```
測試摘要: 總計: 24, 失敗: 0, 成功: 24, 已跳過: 0
```

---

## 🧪 使用 MCP Inspector 測試

MCP Inspector 是官方提供的網頁版除錯工具，可以直接測試 MCP Server 的工具。

### 安裝

```bash
npm i -D @modelcontextprotocol/inspector
```

### 啟動

```bash
cd McpServerHttp
npx @modelcontextprotocol/inspector -- dotnet run
```

成功後會開啟瀏覽器 `http://localhost:6274`，可以：

1. 選擇 **HTTP/SSE** 傳輸模式
2. 輸入 Server URL: `http://localhost:5050/sse`
3. 點擊 **Connect** 按鈕連線
4. **Tools → List Tools** 列出所有工具
5. 直接在網頁上模擬發送 Request 測試工具

---

## 📊 預設測試資料

專案啟動時會自動載入 5 筆測試資料：

| ID | 標題 | 優先級 | 狀態 |
|----|------|--------|------|
| 1 | 完成 MCP Server HTTP 專案 | High | 未完成 |
| 2 | 撰寫單元測試 | High | 未完成 |
| 3 | 更新專案文件 | Normal | 未完成 |
| 4 | 學習 MCP 協定規範 | Normal | ✓ 已完成 |
| 5 | 購買咖啡豆 | Low | 未完成 |

---

## 📝 授權

本專案採用 MIT License
