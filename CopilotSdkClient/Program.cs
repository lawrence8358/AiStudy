using GitHub.Copilot.SDK;

namespace CopilotSdkClient;

class Program
{
    static async Task Main(string[] args)
    {
        // 解析命令列參數或環境變數
        string cliUrl = GetCliUrl(args);

        if (string.IsNullOrEmpty(cliUrl))
        {
            Console.WriteLine("未提供 --url，將啟動或由 SDK 管理本地 Copilot CLI（預設行為）。");
        }
        else
        {
            Console.WriteLine($"正在連接到 Copilot CLI Server: {cliUrl}");
        }
        Console.WriteLine();

        try
        {
            // 建立 Copilot Client（若未提供 URL，使用 SDK 自動啟動本地 CLI）
            var client = string.IsNullOrEmpty(cliUrl)
                ? new CopilotClient()
                : new CopilotClient(new CopilotClientOptions { CliUrl = cliUrl });

            await using (client)
            {
                // 建立 session 並發送請求 
                await using var session = await client.CreateSessionAsync(new SessionConfig { Model = "gpt-5-mini" });
                var response = await session.SendAndWaitAsync(new MessageOptions { Prompt = "你是一個資深軟體工程師，我需要你協助我完成 CodeReview 的工作" });
                Console.WriteLine(response?.Data.Content);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"錯誤: {ex.Message}");
            Console.WriteLine();
            Console.WriteLine("請確認:");
            Console.WriteLine("1. GitHub Copilot CLI 已安裝");
            Console.WriteLine("2. Copilot CLI Server 正在運行 (例如: gh copilot api --listen 4321)");
            Console.WriteLine("3. URL 格式正確");
            Environment.Exit(1);
        }
    }

    static string GetCliUrl(string[] args)
    {
        // 優先使用命令列參數
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == "--url" || args[i] == "-u")
            {
                return args[i + 1];
            }
        }

        // 其次使用環境變數
        return Environment.GetEnvironmentVariable("COPILOT_CLI_URL") ?? "";
    }

    static void PrintUsage()
    {
        Console.WriteLine("GitHub Copilot SDK Client - POC");
        Console.WriteLine();
        Console.WriteLine("用途: 連接到外部 Copilot CLI Server 並發送測試請求");
        Console.WriteLine();
        Console.WriteLine("使用方式:");
        Console.WriteLine("  CopilotSdkClient --url <url>  (--url 可省略以使用本地 CLI)");
        Console.WriteLine();
        Console.WriteLine("參數:");
        Console.WriteLine("  --url, -u    Copilot CLI Server URL");
        Console.WriteLine();
        Console.WriteLine("URL 格式範例:");
        Console.WriteLine("  本地連接:      http://localhost:4321");
        Console.WriteLine("  透過 ngrok:    https://abc123.ngrok.io");
        Console.WriteLine();
        Console.WriteLine("環境變數:");
        Console.WriteLine("  COPILOT_CLI_URL    作為預設 URL (可被 --url 參數覆蓋)");
        Console.WriteLine();
        Console.WriteLine("前置條件:");
        Console.WriteLine("  請先啟動 Copilot CLI Server:");
        Console.WriteLine("  gh copilot api --listen 4321");
    } 
}
