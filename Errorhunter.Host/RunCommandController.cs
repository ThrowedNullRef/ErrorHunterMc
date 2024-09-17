using Microsoft.AspNetCore.Mvc;

namespace Errorhunter.Host;

[ApiController]
[Route("api/run-command")]
public sealed class RunCommandController : ControllerBase
{
    private readonly Func<LibertyBanContext> _createContext;

    public RunCommandController(Func<LibertyBanContext> createContext)
    {
        _createContext = createContext;
    }

    [HttpPost]
    public async Task<ActionResult> RunCommand([FromBody] RunCommandDto dto)
    {
        var client = new ErrorHunterHttpClient();
        await client.RunCommandAsync(dto.Command!);
        return NoContent();
    }
}