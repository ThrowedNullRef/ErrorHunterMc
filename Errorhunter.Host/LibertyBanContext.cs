using Microsoft.EntityFrameworkCore;

namespace Errorhunter.Host;

public sealed class LibertyBanContext : DbContext
{
    public DbSet<LibertyVictim> Victims { get; set; }
    
    public DbSet<LibertyBan> Bans { get; set; }
    
    public DbSet<LibertyName> Names { get; set; }
    
    public DbSet<LibertyPunishment> Punishments { get; set; }
    
    public DbSet<LibertyMute> Mutes { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseMySql(@"Server=45.81.234.14; Port=3306; User ID=u10_1kDXMSFiZL; Password=JphXTvDABCZf9=7zYW+Cpb+A; Database=s10_Bansystem", MySqlServerVersion.LatestSupportedServerVersion);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var victims = modelBuilder.Entity<LibertyVictim>().ToTable("libertybans_victims");
        victims.HasKey(e => e.Id);
        victims.Property(e => e.Uuid);
        victims.HasOne(e => e.Name).WithOne(e => e.Victim).HasForeignKey<LibertyVictim>(e => e.Uuid);
        
        var bans = modelBuilder.Entity<LibertyBan>().ToTable("libertybans_bans");
        bans.HasKey(e => e.Id);
        bans.Property(e => e.VictimId).HasColumnName("Victim");
        bans.HasOne(e => e.Punishment).WithOne(e => e.Ban).HasForeignKey<LibertyBan>(e => e.Id);
        bans.HasOne(e => e.Victim).WithMany(e => e.Bans).HasForeignKey(e => e.VictimId);
        
        var mutes = modelBuilder.Entity<LibertyMute>().ToTable("libertybans_mutes");
        mutes.HasKey(e => e.Id);
        mutes.Property(e => e.VictimId).HasColumnName("Victim");
        mutes.HasOne(e => e.Punishment).WithOne(e => e.Mute).HasForeignKey<LibertyMute>(e => e.Id);
        mutes.HasOne(e => e.Victim).WithMany(e => e.Mutes).HasForeignKey(e => e.VictimId);
        
        var names = modelBuilder.Entity<LibertyName>().ToTable("libertybans_names");
        names.HasKey(e => e.Uuid);
        names.Property(e => e.Name);
        names.Property(e => e.Updated);
        
        var punishments = modelBuilder.Entity<LibertyPunishment>().ToTable("libertybans_punishments");
        punishments.HasKey(e => e.Id);
        punishments.Property(e => e.Start);
        punishments.Property(e => e.End);
        punishments.Property(e => e.Type);
        punishments.Property(e => e.Operator);
        punishments.Property(e => e.Reason);
        punishments.HasOne(e => e.Ban).WithOne(e => e.Punishment).HasForeignKey<LibertyPunishment>(e => e.Id);
        punishments.HasOne(e => e.Mute).WithOne(e => e.Punishment).HasForeignKey<LibertyPunishment>(e => e.Id);
        
        base.OnModelCreating(modelBuilder);
    }
}