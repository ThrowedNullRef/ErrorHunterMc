namespace Errorhunter.Host;

public class LibertyName
{
    public byte[] Uuid { get; set; }
    
    public string Name { get; set; }
    
    public long Updated { get; set; }
    
    public LibertyVictim? Victim { get; set; }
}