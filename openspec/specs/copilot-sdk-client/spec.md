```markdown
# Capability: Copilot SDK Client

透過 GitHub Copilot SDK 連接並控制本地端的 GitHub Copilot CLI Server。

## ADDED Requirements

### Requirement: CLI Server 連接

系統 SHALL 提供連接到外部運行的 GitHub Copilot CLI Server 的能力。

#### Scenario: 成功連接到 CLI Server

- **GIVEN** Copilot CLI 已以 Server 模式運行 (`copilot --server --port 4321`)
- **WHEN** 使用者執行 CopilotSdkClient 程式並指定正確的 Server 位址
- **THEN** 程式成功建立與 CLI Server 的連接

#### Scenario: CLI Server 未運行時的錯誤處理

- **GIVEN** Copilot CLI Server 未運行
- **WHEN** 使用者執行 CopilotSdkClient 程式
- **THEN** 程式顯示友善的錯誤訊息，說明如何啟動 CLI Server

### Requirement: 可配置的連接參數

系統 SHALL 允許使用者透過命令列參數或環境變數配置連接設定。

#### Scenario: 使用預設 Port 連接

- **GIVEN** 使用者未指定自訂 Port
- **WHEN** 執行程式
- **THEN** 程式使用預設 Port (4321) 連接 localhost

#### Scenario: 使用自訂 Port 連接

- **GIVEN** 使用者透過命令列參數指定自訂 Port (例如 `--port 5000`)
- **WHEN** 執行程式
- **THEN** 程式使用指定的 Port 連接

#### Scenario: 使用完整 URL 連接 (支援 HTTPS)

- **GIVEN** 使用者透過命令列參數指定完整 URL (例如 `--url https://abc123.ngrok.io`)
- **WHEN** 執行程式
- **THEN** 程式使用指定的 URL 連接，支援 HTTP 和 HTTPS 協定

#### Scenario: 透過 ngrok 進行遠端連接

- **GIVEN** Copilot CLI Server 透過 ngrok 公開為 HTTPS 端點
- **WHEN** CI 環境執行程式並指定 ngrok URL
- **THEN** 程式成功透過 HTTPS 連接到本地端 CLI Server

### Requirement: 示範功能呼叫

系統 SHALL 提供至少一個示範功能，展示如何透過 SDK 呼叫 Copilot CLI。

#### Scenario: 執行示範功能

- **GIVEN** 程式已成功連接到 CLI Server
- **WHEN** 程式執行示範功能
- **THEN** 顯示來自 Copilot CLI 的回應結果

### Requirement: 使用說明輸出

系統 SHALL 在程式啟動時顯示使用說明。

#### Scenario: 顯示使用說明

- **GIVEN** 使用者執行程式
- **WHEN** 程式啟動
- **THEN** 顯示前置條件說明（需先啟動 CLI Server）和可用的命令列參數

```
