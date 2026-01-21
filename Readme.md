# 🤖 AI Study Project
這是我練習 AI 專用的儲存庫，由於很多功能都是 Preview 版本，所以程式可能會不太穩定，使用上請務必小心


---
## 📚 專案說明
+ AgentFramework：使用 Agent Framework 套件呼叫 Gemini 範例
+ McpServer：[使用 MCP Server 打造企業內部人才搜尋工具 (使用 .NET 10)](https://lawrencetech.blogspot.com/2025/12/mcp-server-net-10.html)


---
## ⚙️ 專案設定檔說明
下面分別列出 `.github` 與 `.vscode` 目錄下的設定檔與用途，讓協作者能更快速理解每個檔案的目的。

### 📁 .github

- **[copilot-instructions.md](.github/copilot-instructions.md)**：GitHub Copilot 專案層級自訂指示檔，用於提供專案特定的編碼風格、慣例或上下文給 Copilot。

#### 💬 prompts/

- **[copilot-commit-message-instructions.md](.github/prompts/copilot-commit-message-instructions.md)**：Copilot 產生 commit 訊息時的提示範本。

#### 🛠️ skills/

- **[make-skill-template](.github/skills/make-skill-template/SKILL.md)**：從 [awesome-copilot](https://github.com/github/awesome-copilot) 下載的 Agent Skill 範本，用於建立新的 GitHub Copilot Agent Skills。
- **[pdf](.github/skills/pdf/SKILL.md)**：從 [Anthropic 官方技能](https://github.com/anthropics/skills) 下載的 PDF 處理技能，提供完整的 PDF 操作功能，包含文字與表格擷取、PDF 建立、合併/分割文件、表單處理等。

### 🔧 .vscode

- **[settings.json](.vscode/settings.json)**：VS Code 工作區設定檔，包含本地開發與擴充套件行為設定，例如 Copilot Chat 產生 commit 訊息時的提示範本參考路徑。
- **[mcp.json](.vscode/mcp.json)**：MCP Server 啟動設定檔，定義本地開發時的伺服器啟動命令（例如：`McpServerDemo` 專案）。

---
## 🚀 啟用 GitHub Copilot Agent Skills

若要讓 GitHub Copilot Chat 能夠使用專案中的 Skills，建議在工作區（workspace）層級啟用 `chat.useAgentSkills`，這樣設定會儲存在專案的 `.vscode/settings.json` 中，對所有協作者生效。

### 📝 設定步驟

1. 開啟 VS Code 的設定頁面（`Ctrl+,` 或 `Cmd+,`）
2. 在右上角選擇 `Workspace`
3. 搜尋 `chat.useAgentSkills`
4. 勾選「Chat › Use Agent Skills」或將下列設定加入 `.vscode/settings.json`（工作區設定）：

   ```json
   {
     "chat.useAgentSkills": true
   }
   ```

### ⚠️ 注意事項

- 啟用 `chat.useAgentSkills` 後，Copilot Chat 就能使用專案中 `.github/skills/` 資料夾內的自訂技能。這些技能會在對話時自動被偵測並使用，無需額外設定。
- 上述設定步驟會將設定寫入工作區的 `.vscode/settings.json`，所有協作者都會套用。如果只想自己使用，可以改為設定在個人的使用者設定中（不提交到版本控制）。


---
## 🔌 設定 MCP Server

MCP (Model Context Protocol) Server 讓 AI 助手能夠存取專案特定的工具和資料。本專案已在 `.vscode/mcp.json` 中預先設定了 `McpServerDemo`。

### 📝 設定步驟

1. 確認 `.vscode/mcp.json` 檔案存在且包含以下設定：

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
     },
     "inputs": []
   }
   ```

2. 確保已安裝 .NET SDK（本專案使用 .NET 10）
3. 重新啟動 VS Code 讓設定生效

### ✅ 驗證設定

完成設定後，可透過以下方式驗證 MCP Server 是否成功啟用：

#### 方法一：透過 GitHub Copilot Chat

1. 開啟 GitHub Copilot Chat（`Ctrl+Alt+I` 或 `Cmd+Option+I`）
2. 在聊天視窗中，點擊輸入框上方的「附加」按鈕（📎 圖示）
3. 查看是否出現 `McpServerDemo` 選項
4. 如果看到該選項，表示 MCP Server 已成功註冊

#### 方法二：查看 VS Code 輸出面板

1. 開啟輸出面板（`View` > `Output` 或 `Ctrl+Shift+U`）
2. 在下拉選單中選擇「GitHub Copilot Chat」或「MCP」
3. 查看是否有 MCP Server 啟動的相關日誌訊息
4. 若看到類似 `Connected to McpServerDemo` 的訊息，表示連線成功

#### 方法三：直接測試功能

1. 在 Copilot Chat 中詢問需要使用 MCP Server 提供的功能
2. 觀察 Copilot 是否能正確呼叫 Server 並回傳結果

### ⚠️ 注意事項

- MCP Server 需要在 VS Code 啟動時自動載入，若修改 `mcp.json` 後需重新啟動 VS Code
- 確保 `McpServer.csproj` 專案可以正常執行（可先手動執行 `dotnet run` 測試）
- 若 Server 無法啟動，請檢查輸出面板的錯誤訊息進行除錯

---
## 🔗 參考資源
+ [Awesome Agent Skills 說明](https://github.com/heilcheng/awesome-agent-skills/blob/main/README.zh-TW.md)
+ [ihower AI 開發課程](https://ihower.tw/blog/posts)
+ [Introduction to GenAI and ML 2025 Fall - 李宏毅](https://speech.ee.ntu.edu.tw/~hylee/GenAI-ML/2025-fall.php)
+ [Claude Code Skills：讓 AI 變身專業工匠](https://kaochenlong.com/claude-code-skills)
+ [VS Code 中 GitHub Copilot 設定參考](https://vscode.com.tw/docs/copilot/reference/copilot-settings#_general-settings)

+ 各大好用的 Skill 範例
  + [Anthropic 官方技能](https://github.com/anthropics/skills)
  + [OpenAI Codex 官方技能](https://github.com/openai/skills)
  + [awesome-copilot](https://github.com/github/awesome-copilot)


---
### License
The MIT license

 