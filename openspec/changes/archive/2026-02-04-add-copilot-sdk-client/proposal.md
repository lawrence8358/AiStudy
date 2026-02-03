# Change: 新增 GitHub Copilot SDK 用戶端專案

## Why

目前專案缺乏與 GitHub Copilot CLI 整合的功能。透過 GitHub Copilot SDK，可以讓外部服務程式化地控制和呼叫本地端的 GitHub Copilot CLI，這對於 AI 工具整合測試和自動化流程非常有價值。

## What Changes

- 新增 `CopilotSdkClient` 專案 (C# .NET 10 控制台應用程式)
- 整合 `GitHub.Copilot.SDK` NuGet 套件 (v0.1.20)
- 實作連接外部 Copilot CLI Server 的功能
- 提供簡單的 POC 範例，展示如何透過 SDK 呼叫 Copilot CLI
- 更新 `AiStudy.slnx` 方案檔以包含新專案
- 更新 `Readme.md` 文件說明新專案用途

## Impact

- 受影響的 specs: 新增 `copilot-sdk-client` capability
- 受影響的程式碼:
  - 新增 `CopilotSdkClient/` 專案目錄
  - 修改 `AiStudy.slnx` 方案檔
  - 修改 `Readme.md` 文件

## 相關資源

- [GitHub Copilot SDK 文件](https://github.com/github/copilot-sdk/blob/main/docs/getting-started.md#connecting-to-an-external-cli-server)
- [GitHub.Copilot.SDK NuGet 套件](https://www.nuget.org/packages/GitHub.Copilot.SDK)
