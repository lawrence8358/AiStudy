# Tasks: HTTP MCP Server 實作任務

## 1. 專案建立
- [x] 1.1 建立 `McpServerHttp/` 目錄結構
- [x] 1.2 建立 `McpServerHttp.csproj` 專案檔
- [x] 1.3 建立 `McpServerHttp/Properties/launchSettings.json`

## 2. 核心實作
- [x] 2.1 建立 `McpServerHttp/Models/TodoModel.cs` 資料模型
- [x] 2.2 建立 `McpServerHttp/Tools/TodoTool.cs` MCP 工具
- [x] 2.3 建立 `McpServerHttp/Program.cs` 伺服器進入點

## 3. 單元測試
- [x] 3.1 建立 `McpServerHttp.Tests/` 測試專案目錄
- [x] 3.2 建立 `McpServerHttp.Tests.csproj` 測試專案檔
- [x] 3.3 建立 `McpServerHttp.Tests/TodoToolTests.cs` 測試類別
- [x] 3.4 執行測試確認通過 (`dotnet test`)

## 4. 方案整合
- [x] 4.1 更新 `AiStudy.slnx` 加入新專案

## 5. 文件撰寫
- [x] 5.1 建立 `McpServerHttp/README.md` 專案說明文件
- [x] 5.2 更新根目錄 `Readme.md` 加入專案說明

## 6. 驗證
- [x] 6.1 執行 `dotnet build` 確認建置成功
- [x] 6.2 執行 `dotnet test` 確認測試通過
- [x] 6.3 啟動服務確認可正常運行 (`dotnet run --project McpServerHttp`)
