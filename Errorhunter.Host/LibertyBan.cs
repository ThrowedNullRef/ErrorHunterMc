namespace Errorhunter.Host;

public class LibertyBan 
{
    public long Id { get; set; }
    
    public long VictimId { get; set; }
    
    public LibertyPunishment? Punishment { get; set; }

    public LibertyVictim? Victim { get; set; }
}