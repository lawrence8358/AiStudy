```markdown
# Design: GitHub Copilot SDK 用戶端

## Context

GitHub Copilot SDK 是一個允許程式化控制 GitHub Copilot CLI 的套件。本專案需要一個簡單的 POC (Proof of Concept) 專案來驗證 SDK 與本地 Copilot CLI Server 的連接能力。

### 背景

- GitHub Copilot CLI 可以以 Server 模式運行，監聽指定的 Port
- GitHub.Copilot.SDK 提供 .NET 版本的用戶端，可連接到運行中的 CLI Server
- 此為實驗性專案，需保持架構簡單

### 約束條件

- 必須使用 .NET 10 以符合專案既有慣例
- GitHub.Copilot.SDK 最低需求為 .NET 8.0
- Copilot CLI 必須預先安裝並以 Server 模式運行

## Goals / Non-Goals

### Goals

- 建立可運作的 POC 專案，驗證 SDK 連接功能
- 保持程式碼簡單，易於理解和修改
- 提供清晰的使用說明文件

### Non-Goals

- 不實作完整的生產級錯誤處理
- 不建立複雜的抽象層或架構模式
- 不包含單元測試（POC 階段）

## Decisions

### 決策 1: 專案類型選擇

**選擇**: 控制台應用程式 (Console Application)

**原因**:
- 最簡單的專案類型，適合 POC
- 易於執行和測試
- 不需要額外的 Web 框架或 UI 框架

**替代方案考慮**:
- ASP.NET Core Web API: 過於複雜，不符合 POC 需求
- Worker Service: 適合長期運行的服務，但對 POC 來說過度設計

### 決策 2: CLI Server 連接方式

**選擇**: 使用 `CopilotClientOptions.CliUrl` 連接外部 Server

**原因**:
- 符合 SDK 文件建議的做法
- 允許 CLI 獨立運行，便於除錯
- 支援多個用戶端連接同一個 Server

**程式碼範例**:
```csharp
using var client = new CopilotClient(new CopilotClientOptions
{
    CliUrl = "localhost:4321",
});
```

### 決策 3: 支援 HTTPS 連接

**選擇**: 支援完整 URL 格式，包含 HTTP 和 HTTPS

**原因**:
- 允許透過 ngrok 等工具公開本地服務
- 支援 CI 環境遠端呼叫本地端 Copilot CLI Server
- URL 格式更直觀，例如 `https://abc123.ngrok.io`

**程式碼範例**:
```csharp
// 本地連接
using var client = new CopilotClient(new CopilotClientOptions
{
    CliUrl = "http://localhost:4321",
});

// 透過 ngrok 連接
using var client = new CopilotClient(new CopilotClientOptions
{
    CliUrl = "https://abc123.ngrok.io",
});
```

### 決策 4: 配置管理

**選擇**: 使用命令列參數或環境變數

**原因**:
- 保持簡單，不需要額外的設定檔
- 適合 POC 和開發測試場景
- 易於在不同環境中調整

## Risks / Trade-offs

| 風險 | 緩解措施 |
|------|----------|
| Copilot CLI 未安裝或未運行 | 提供清晰的前置條件說明 |
| SDK 套件版本不穩定 (0.1.x) | 固定套件版本，避免自動升級 |
| Port 衝突 | 允許自訂 Port 設定 |

## Migration Plan

不適用 - 此為新增專案，無需遷移。

## Open Questions

1. ~~是否需要支援 HTTPS 連接?~~ - 決定：支援，以便透過 ngrok 讓 CI 測試呼叫本地端服務
2. ~~是否需要實作重連機制?~~ - 決定：暫不需要，保持簡單

```
