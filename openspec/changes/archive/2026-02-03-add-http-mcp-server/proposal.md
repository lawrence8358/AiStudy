# Change: 新增 HTTP MCP Server 專案（待辦事項管理）

## Why

現有的 `McpServer` 專案使用 Stdio 傳輸協定，僅能透過 Console 模式與 AI Client 溝通。為了支援更多樣化的整合場景（如 Web 應用程式、遠端服務呼叫），需要建立一個基於 HTTP/SSE (Server-Sent Events) 傳輸的 MCP Server。同時，為了區分不同的 MCP 工具調用場景，將實作「待辦事項管理」功能作為測試情境，與現有的「員工與工作經歷查詢」形成對比。

## What Changes

- **[NEW]** `McpServerHttp/` - 新增獨立的 HTTP MCP Server 專案
  - 使用 ASP.NET Core Minimal API
  - 使用 `ModelContextProtocol` 0.6.0-preview.1 的 SSE 傳輸
  - 實作 `TodoTool` 提供待辦事項 CRUD 操作
- **[NEW]** `McpServerHttp.Tests/` - 對應的單元測試專案
- **[MODIFIED]** `AiStudy.slnx` - 將新專案加入方案
- **[MODIFIED]** `Readme.md` - 新增 McpServerHttp 專案說明

## Impact

- Affected specs: `http-mcp-server` (新增)
- Affected code:
  - `McpServerHttp/Program.cs` - 伺服器進入點
  - `McpServerHttp/Tools/TodoTool.cs` - 待辦事項工具
  - `McpServerHttp/Models/TodoModel.cs` - 資料模型
  - `McpServerHttp/README.md` - 專案文件
  - `McpServerHttp.Tests/` - 單元測試
