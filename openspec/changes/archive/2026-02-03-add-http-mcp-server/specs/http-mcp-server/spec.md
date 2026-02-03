# HTTP MCP Server - 待辦事項管理

## ADDED Requirements

### Requirement: HTTP MCP Server 傳輸
系統 SHALL 提供基於 HTTP/SSE 傳輸協定的 MCP Server，允許 AI Client 透過 HTTP 連線調用工具。

#### Scenario: 成功啟動 HTTP 伺服器
- **WHEN** 執行 `dotnet run --project McpServerHttp`
- **THEN** 伺服器在設定的 Port (預設 5050) 成功啟動
- **AND** 可接受 MCP Client 的 SSE 連線

---

### Requirement: 取得所有待辦事項
系統 SHALL 提供 `GetAllTodos` 工具，回傳所有待辦事項清單。

#### Scenario: 取得待辦事項清單
- **WHEN** AI Client 調用 `GetAllTodos` 工具
- **THEN** 系統回傳所有待辦事項的陣列

---

### Requirement: 依 ID 取得待辦事項
系統 SHALL 提供 `GetTodoById` 工具，依指定 ID 回傳單一待辦事項。

#### Scenario: 成功取得待辦事項
- **WHEN** AI Client 調用 `GetTodoById` 並傳入有效的 ID
- **THEN** 系統回傳對應的待辦事項物件

#### Scenario: ID 不存在
- **WHEN** AI Client 調用 `GetTodoById` 並傳入不存在的 ID
- **THEN** 系統回傳 null 或錯誤訊息

---

### Requirement: 新增待辦事項
系統 SHALL 提供 `AddTodo` 工具，新增一筆待辦事項。

#### Scenario: 成功新增待辦事項
- **WHEN** AI Client 調用 `AddTodo` 並傳入標題與描述
- **THEN** 系統建立新的待辦事項
- **AND** 回傳新建立的待辦事項（含自動產生的 ID）

---

### Requirement: 更新待辦事項
系統 SHALL 提供 `UpdateTodo` 工具，更新指定 ID 的待辦事項。

#### Scenario: 成功更新待辦事項
- **WHEN** AI Client 調用 `UpdateTodo` 並傳入有效的 ID 與更新資料
- **THEN** 系統更新對應的待辦事項
- **AND** 回傳更新後的待辦事項物件

---

### Requirement: 刪除待辦事項
系統 SHALL 提供 `DeleteTodo` 工具，刪除指定 ID 的待辦事項。

#### Scenario: 成功刪除待辦事項
- **WHEN** AI Client 調用 `DeleteTodo` 並傳入有效的 ID
- **THEN** 系統刪除對應的待辦事項
- **AND** 回傳刪除成功訊息

---

### Requirement: 搜尋待辦事項
系統 SHALL 提供 `SearchTodos` 工具，依關鍵字搜尋待辦事項。

#### Scenario: 搜尋符合的待辦事項
- **WHEN** AI Client 調用 `SearchTodos` 並傳入關鍵字
- **THEN** 系統回傳標題或描述包含關鍵字的待辦事項清單

---

### Requirement: 切換待辦事項狀態
系統 SHALL 提供 `ToggleTodoStatus` 工具，切換待辦事項的完成狀態。

#### Scenario: 成功切換狀態
- **WHEN** AI Client 調用 `ToggleTodoStatus` 並傳入有效的 ID
- **THEN** 系統將該待辦事項的 `IsCompleted` 狀態反轉
- **AND** 回傳更新後的待辦事項物件
