# 🔌 MCP Server - 企業內部人才搜尋工具

這是一個使用 .NET 10 開發的 MCP (Model Context Protocol) Server 範例專案，模擬企業內部的人才搜尋工具，讓 AI Client（如 GitHub Copilot、Claude Desktop）能夠透過標準化的 MCP 協定查詢員工資料與工作經歷。

> 📖 **相關文章**：[使用 MCP Server 打造企業內部人才搜尋工具 (使用 .NET 10)](https://lawrencetech.blogspot.com/2025/12/mcp-server-net-10.html)

---

## 📚 專案說明

### 什麼是 MCP？

MCP (Model Context Protocol) 是一個標準化的協定，用來連接 AI 模型（MCP Client）與資料來源/工具（MCP Server）。透過 MCP，開發者只需要寫一次程式，就能被支援 MCP 的各種 Client 使用，避免針對不同 AI 平台重複開發整合介面。

### 功能特色

本專案提供兩大類工具：

1. **員工資料查詢**（EmployeeTool）
   - 取得所有員工清單
   - 依員工編號查詢
   - 依姓名（中文或英文）查詢
   - 依部門搜尋員工

2. **工作經歷查詢**（JobHistoryTool）
   - 依員工編號取得完整工作經歷

---

## 🛠️ 開發語言與技術棧

### 開發環境

- **.NET 10**（也支援 .NET 8/9）
- **C# 13**
- **Visual Studio Code** 或 **Visual Studio 2022**

### 使用套件

| 套件名稱 | 版本 | 用途 |
|---------|------|------|
| `Microsoft.Extensions.Hosting` | 10.0.0 | 建立標準的 .NET Host |
| `ModelContextProtocol` | 0.5.0-preview.1 | MCP Server 核心套件（Preview 版本） |

```xml
<PackageReference Include="Microsoft.Extensions.Hosting" Version="10.0.0" />
<PackageReference Include="ModelContextProtocol" Version="0.5.0-preview.1" />
```

> ⚠️ **注意**：`ModelContextProtocol` 目前為 Preview 版本，API 可能會變動，請留意官方更新。

---

## 🔧 MCP Server 架構說明

### Program.cs - 伺服器進入點

```csharp
var builder = Host.CreateEmptyApplicationBuilder(settings: null);

// 註冊 MCP Server 服務
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()  // 使用 Stdio 進行傳輸
    .WithToolsFromAssembly();    // 自動掃描組件中的 Tools

await builder.Build().RunAsync();
```

**為什麼使用 `CreateEmptyApplicationBuilder`？**

一般的 `CreateApplicationBuilder` 會預設載入許多設定檔（appsettings.json）、環境變數與預設服務。但 MCP Server 是輕量級的 Console App，透過 Stdio 與 Client 溝通，使用 `CreateEmptyApplicationBuilder` 可以：
- 加快啟動速度
- 減少不必要的依賴
- 降低干擾因素

### Tools 定義 - 使用 Attribute 標記

MCP Server 透過 Attribute 來定義可被 AI 呼叫的工具：

| Attribute | 用途 |
|-----------|------|
| `[McpServerToolType]` | 標記類別為 MCP 工具容器 |
| `[McpServerTool]` | 標記方法為可呼叫的工具 |
| `[Description]` | 用自然語言描述工具功能與參數（AI 依此決定何時呼叫） |

---

## 📋 開放的 MCP 工具

### 1. EmployeeTool - 員工資料查詢

#### `GetAll()` - 取得所有員工清單

```csharp
[McpServerTool, Description("取得所有員工清單")]
public static IEnumerable<EmployeeModel> GetAll()
```

回傳包含 `empNo`、`nameZh`、`nameEn`、`department`、`position` 的員工物件陣列。

#### `GetById(empno)` - 依員工編號查詢

```csharp
[McpServerTool, Description("依員工編號取得員工資訊")]
public static EmployeeModel? GetById([Description("員工編號")] string empno)
```

#### `GetInfo(name)` - 依姓名查詢

```csharp
[McpServerTool, Description("依姓名（中文或英文）取得員工資訊字串")]
public static string GetInfo([Description("員工中文或英文姓名")] string name)
```

回傳格式化的員工資訊字串。

#### `SearchByDepartment(department)` - 依部門搜尋

```csharp
[McpServerTool, Description("依部門搜尋員工列表")]
public static IEnumerable<EmployeeModel> SearchByDepartment([Description("部門名稱")] string department)
```

### 2. JobHistoryTool - 工作經歷查詢

#### `GetJobHistoryByEmpNo(empNo)` - 取得工作經歷

```csharp
[McpServerTool, Description("取得指定員工的工作經歷")]
public static IEnumerable<JobHistory> GetJobHistoryByEmpNo([Description("員工編號")] string empNo)
```

回傳包含 `empNo`、`company`、`position`、`startDate`、`endDate`、`description` 的工作經歷物件陣列。

---

## 🚀 使用範例

### AI Client 執行流程

當使用者詢問員工相關資訊時，AI Client 會：

1. 分析使用者問題，決定需要呼叫哪些 MCP 工具
2. 依序呼叫對應的工具（例如：先查詢員工資訊取得編號，再查詢工作經歷）
3. 整理 MCP Server 回傳的資料
4. 以易讀的格式呈現給使用者

> 💡 實際的員工資料儲存在 MCP Server 中，請透過 AI Client 呼叫工具來查詢

---

## 🧪 開發與除錯

### 使用 MCP Inspector

MCP Inspector 是官方提供的網頁版除錯工具，可以直接測試 MCP Server 的工具。

#### 安裝

```bash
npm i -D @modelcontextprotocol/inspector
```

#### 啟動

```bash
cd McpServer
npx @modelcontextprotocol/inspector dotnet run
```

成功後會開啟瀏覽器 `http://localhost:6274`，可以：
1. 點擊 **Connect** 按鈕連線
2. **Tools → List Tools** 列出所有工具
3. 直接在網頁上模擬發送 Request 測試工具

---

## 🔗 與 GitHub Copilot 整合

### 設定步驟

詳細設定步驟請參考主專案的 [README.md](../Readme.md#-設定-mcp-server) 中的「設定 MCP Server」章節。

### 快速設定

確保 `.vscode/mcp.json` 包含以下設定：

```json
{
  "servers": {
    "McpServerDemo": {
      "type": "stdio",
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "${workspaceFolder}/McpServer/McpServer.csproj"
      ]
    }
  }
}
```

### 驗證設定

1. 開啟 GitHub Copilot Chat
2. 點擊輸入框上方的「附加」按鈕（📎 圖示）
3. 查看是否出現 `McpServerDemo` 選項
4. 若出現表示設定成功！

---

## 📝 授權

本專案採用 MIT License
