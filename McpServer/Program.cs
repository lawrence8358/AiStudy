using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// 這裡使用 CreateEmptyApplicationBuilder 而不是 CreateApplicationBuilder
var builder = Host.CreateEmptyApplicationBuilder(settings: null);

// // 設定 Log 等級，方便除錯
// builder.Logging.AddConsole(consoleLogOptions =>
// {
//     consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
// });

// 註冊 MCP Server 服務
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport() // 使用 Stdio 進行傳輸
    .WithToolsFromAssembly();   // 自動掃描組件中的 Tools

await builder.Build().RunAsync();