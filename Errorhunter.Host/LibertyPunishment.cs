namespace Errorhunter.Host;

public class LibertyPunishment
{
    public long Id { get; set; }
    
    public int Type { get; set; }
    
    public byte[] Operator { get; set; }
    
    public string Reason { get; set; }
    
    public long Start { get; set; }
    
    public long End { get; set; }
    
    public LibertyBan? Ban { get; set; }
    
    public LibertyMute? Mute { get; set; }
}