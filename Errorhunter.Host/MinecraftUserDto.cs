namespace Errorhunter.Host;

public sealed class MinecraftUserDto
{
    public MinecraftUserDto(long id, List<UserPunishmentDto> punishments, string userName)
    {
        Id = id;
        Punishments = punishments;
        UserName = userName;
    }

    public long Id { get; }

    public List<UserPunishmentDto> Punishments { get; }

    public string UserName { get; }

    public static MinecraftUserDto FromName(LibertyName name)
    {
        var punishments = new List<UserPunishmentDto>();
        if (name.Victim is not null)
        {
            var victim = name.Victim;
            punishments.AddRange(victim.Bans.Select(UserPunishmentDto.FromBan));
            punishments.AddRange(victim.Mutes.Select(UserPunishmentDto.FromMute));
        }

        return new (BitConverter.ToInt64(name.Uuid),
                    punishments,
                    name.Name);
    }
}