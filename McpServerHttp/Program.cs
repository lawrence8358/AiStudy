using McpServerHttp.Repositories;
using McpServerHttp.Tools;

var builder = WebApplication.CreateBuilder(args);

// 註冊 Repository (Singleton 確保資料一致性)
builder.Services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();

// 註冊 TodoTool (由 DI 容器管理)
builder.Services.AddScoped<TodoTool>();

// 註冊 MCP Server 服務
builder.Services
    .AddMcpServer()
    .WithHttpTransport()  // 使用 HTTP/SSE 傳輸
    .WithTools<TodoTool>();

// 設定 CORS (僅在開發環境使用寬鬆設定)
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors();
}

// 健康檢查端點 (避免與 MapMcp 的根路徑衝突)
app.MapGet("/info", () => "MCP Server HTTP (Todo) is running!");
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

// 設定 MCP SSE 端點 (會註冊 "/" 和 "/sse" 等端點)
app.MapMcp();

Console.WriteLine("MCP Server HTTP started");
Console.WriteLine("MCP Endpoint: http://localhost:5050/sse");
Console.WriteLine("Available tools: GetAllTodos, GetTodoById, AddTodo, UpdateTodo, DeleteTodo, SearchTodos, ToggleTodoStatus");

app.Run();
