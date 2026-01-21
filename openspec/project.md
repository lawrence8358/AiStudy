# Project Context

## Purpose
這是一個 **AI 功能學習與實驗專案**，專門用於：
- 測試 AI Agent 對外部工具、API 及 MCP Server 的調用能力
- 探索 .NET 10 生態系統中的 AI/LLM 整合方案
- 驗證 GitHub Copilot 的 Agent Skills 和 MCP 協議整合

## Tech Stack
- **主要語言**：C# / .NET 10
- **方案格式**：`.slnx`
- **AI/LLM 整合**：
  - `ModelContextProtocol` (MCP SDK) v0.5.0-preview
  - `Microsoft.Agents.AI.OpenAI` v1.0.0-preview
- **開發工具**：
  - Visual Studio Code
  - GitHub Copilot (含 Agent Skills)
  - MCP Server 整合

## Project Conventions

### Code Style
- **語言**：程式碼使用英文，文件與註解使用繁體中文 (zh-TW)
- **Nullable**：啟用可空參考型別 (`<Nullable>enable</Nullable>`)
- **Implicit Usings**：啟用隱式 Using (`<ImplicitUsings>enable</ImplicitUsings>`)
- **命名慣例**：遵循 .NET 標準命名慣例
  - `PascalCase`：類別、方法、屬性
  - `camelCase`：區域變數、參數
  - `_camelCase`：私有欄位

### Architecture Patterns
- **專案結構**：依功能模組分離為獨立專案
  - `McpServer/`：MCP Server 實作，提供 AI 工具調用功能
  - `AgentFramework/`：Agent Framework 與 Gemini API 整合範例
- **工具設計**：MCP Tools 放置於 `McpServer/Tools/` 目錄下

### Testing Strategy
- 此為實驗性專案，以手動測試和驗證為主
- MCP Server 功能透過 Copilot Chat 直接測試調用
- 確保每個 MCP Tool 可被 AI 正確識別並調用

### Git Workflow
- **主分支**：`main`
- **Commit 訊息**：使用繁體中文，遵循 `.github/prompts/copilot-commit-message-instructions.md` 規範
- **License**：MIT License

## Domain Context
- **MCP (Model Context Protocol)**：一種讓 AI 助手存取專案特定工具和資料的協議
- **Agent Skills**：GitHub Copilot 的擴展功能，透過 `.github/skills/` 目錄定義
- 專案主要目的是「測試 AI 工具調用」而非一般應用程式開發

## Important Constraints
- 使用 .NET 10 Preview 版本，可能存在不穩定因素
- MCP SDK 仍在 Preview 階段 (v0.5.0-preview.1)
- Microsoft Agents AI SDK 也在 Preview 階段

## External Dependencies
- **Google Gemini API**：透過 Agent Framework 調用
- **GitHub Copilot**：提供 AI 對話與 Agent Skills 功能
- **MCP Server**：透過 VS Code 整合，提供自定義工具給 AI 助手
