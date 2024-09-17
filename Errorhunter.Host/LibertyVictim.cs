namespace Errorhunter.Host;

public class LibertyVictim
{
    public long Id { get; set; }
    
    public byte[] Uuid { get; set; }
    
    public LibertyName? Name { get; set; }
    
    public List<LibertyBan> Bans { get; set; }
    
    public List<LibertyMute> Mutes { get; set; }
}