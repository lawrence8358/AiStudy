```markdown
# Tasks: 新增 GitHub Copilot SDK 用戶端專案

## 1. 專案建立

- [x] 1.1 建立 `CopilotSdkClient` 控制台專案目錄
- [x] 1.2 建立 `.csproj` 專案檔 (目標框架 net10.0)
- [x] 1.3 新增 `GitHub.Copilot.SDK` NuGet 套件參考 (v0.1.20)
- [x] 1.4 將專案加入 `AiStudy.slnx` 方案檔

## 2. 程式碼實作

- [x] 2.1 建立 `Program.cs` 主程式
- [x] 2.2 實作 CopilotClient 連接邏輯 (支援完整 URL，包含 HTTP/HTTPS)
- [x] 2.3 實作簡單的示範呼叫功能
- [x] 2.4 新增基本的錯誤處理和使用說明輸出
- [x] 2.5 支援 `--url` 參數以接受完整 URL (如 ngrok HTTPS 端點)

## 3. 驗證

- [x] 3.1 執行 `dotnet build` 確認專案建置成功
- [x] 3.2 驗證程式可正確執行（需預先啟動 Copilot CLI Server）

## 4. 文件更新

- [x] 4.1 更新 `Readme.md` 新增專案說明
- [x] 4.2 新增 Copilot CLI Server 啟動說明
- [x] 4.3 新增程式執行範例說明 (包含 ngrok HTTPS 使用範例)

## 依賴關係

```
1.1 → 1.2 → 1.3 → 1.4 (循序)
1.4 → 2.1 → 2.2 → 2.3 → 2.4 (循序)
2.4 → 3.1 → 3.2 (循序)
3.2 → 4.1 → 4.2 → 4.3 (循序)
```

## 驗收標準

- [x] 專案可成功建置 (`dotnet build` 無錯誤)
- [x] 程式可連接到本地 Copilot CLI Server
- [x] 程式支援 HTTPS URL (適用於 ngrok 等場景)
- [x] Readme.md 包含完整的使用說明

```
