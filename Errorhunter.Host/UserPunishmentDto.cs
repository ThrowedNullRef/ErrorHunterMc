namespace Errorhunter.Host;

public sealed class UserPunishmentDto
{
    public UserPunishmentDto(long id, PunishmentType type, string reason, DateTime from, DateTime? to)
    {
        Id = id;
        Type = type;
        Reason = reason;
        From = from;
        To = to;
    }
    
    public long Id { get; }
        
    public PunishmentType Type { get; }
    
    public string Reason { get; }
    
    public DateTime From { get; }
    
    public DateTime? To { get; }
    
      public static UserPunishmentDto FromMute(LibertyMute mute) =>
        new (mute.Id,
             PunishmentType.Mute,
             mute.Punishment!.Reason,
             DateTimeParser.FromUnix(mute.Punishment.Start),
             mute.Punishment.End == 0 ? null : DateTimeParser.FromUnix(mute.Punishment.End));

    public static UserPunishmentDto FromBan(LibertyBan ban) =>
        new (ban.Id,
             PunishmentType.Ban,
             ban.Punishment!.Reason,
             DateTimeParser.FromUnix(ban.Punishment.Start),
             ban.Punishment.End == 0 ? null : DateTimeParser.FromUnix(ban.Punishment.End));
}