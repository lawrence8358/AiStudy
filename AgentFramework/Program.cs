using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Chat;

string instructions = "你是一個專門協助程式碼審閱的資深程式設計師，請針對程式碼簡單的回應代碼可能的問題，在回答問題請，請先表明你是 [PEX AIBot]";
var badCode = @"var password = 'admin';";

var result = await RunAiCodeReview(badCode);
Console.WriteLine("======= 寫法一 =======\n" + result);

var agent = CreateAiCodeReviewAgent();
result = (await agent.RunAsync(badCode)).Text;
Console.WriteLine("======= 寫法二 =======\n" + result);

async Task<string> RunAiCodeReview(string code)
{
    using var chatClient = new ChatClient(
      model: "gemini-2.5-flash",
      credential: GetMeminiAPiKey(),
      options: GetAIClientOptions()
    )
    .AsIChatClient();

    var agent = new ChatClientAgent(chatClient, name: "CodeReviewer", instructions: instructions);
    var result = await agent.RunAsync(code);

    return result.Text;
}

AIAgent CreateAiCodeReviewAgent()
{
    AIAgent agent = new ChatClient(
        model: "gemini-2.5-flash",
        credential: GetMeminiAPiKey(),
        options: GetAIClientOptions()
    )
    .CreateAIAgent(name: "CodeReviewer", instructions: instructions);

    return agent;
}

System.ClientModel.ApiKeyCredential GetMeminiAPiKey()
{
    return new System.ClientModel.ApiKeyCredential(Environment.GetEnvironmentVariable("GEMINI_API_KEY") ?? "");
}

OpenAIClientOptions GetAIClientOptions()
{
    return new OpenAIClientOptions
    {
        // Gemini 預設的回傳格式和 OpenAi 不同，但有另外提供一個相容性的接口
        Endpoint = new Uri("https://generativelanguage.googleapis.com/v1beta/openai/")
    };
}
