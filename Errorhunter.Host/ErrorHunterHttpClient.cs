using System.Net.Http.Headers;

namespace Errorhunter.Host;

public sealed class ErrorHunterHttpClient
{
    private readonly HttpClient _client;
    
    public ErrorHunterHttpClient()
    {
        _client = new ()
        {
            BaseAddress = new Uri("https://panel.errorhunter.de"),
        };
        
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
    }

    public async Task RunCommandAsync(string command)
    {
        var body = new RunCommandBody()
        {
            Command = command
        };
        var jsonBody = JsonContent.Create(body);
        await _client.PostAsync("api/client/servers/60f24217/command", jsonBody);
    }
}