using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Errorhunter.Host;

[ApiController]
[Route("api/minecraft-users")]
public sealed class MinecraftUsersController : ControllerBase
{
    private readonly Func<LibertyBanContext> _createContext;

    public MinecraftUsersController(Func<LibertyBanContext> createContext)
    {
        _createContext = createContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<MinecraftUserDto>>> GetMinecraftUsers([FromQuery] string? searchTerm, [FromQuery] bool showOnlyWithPunishments)
    {
        searchTerm = searchTerm?.ToLower();
        
        await using var context = _createContext();
        var query = context.Names
                           .Include(n => n.Victim).ThenInclude(v => v.Bans).ThenInclude(b => b.Punishment)
                           .Include(n => n.Victim).ThenInclude(v => v.Mutes).ThenInclude(b => b.Punishment)
                           .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
            query = query.Where(n => n.Name.ToLower().Contains(searchTerm)).AsQueryable();

        if (showOnlyWithPunishments)
            query = query.Where(n => n.Victim.Bans.Any() || n.Victim.Mutes.Any()).AsQueryable();

        var users = await query.OrderByDescending(n => n.Updated).ToListAsync();
                            
        var response = users.Select(MinecraftUserDto.FromName).ToList();
        return Ok(response);
    }
}