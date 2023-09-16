using Microsoft.EntityFrameworkCore;
using Twitter_Clone.Models;

namespace Twitter_Clone.Data;

public class TwitterCloneDb : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Tweet> Tweets { get; set; }

    public DbSet<UserFollow> UserFollows { get; set; }

    public TwitterCloneDb(DbContextOptions<TwitterCloneDb> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Tweets)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<UserFollow>()
            .HasKey(uf => new { uf.FollowerId, uf.FollowingId });

        modelBuilder.Entity<UserFollow>()
            .HasOne(uf => uf.Follower)
            .WithMany(u => u.Following)
            .HasForeignKey(uf => uf.FollowerId);

        modelBuilder.Entity<UserFollow>()
            .HasOne(uf => uf.Following)
            .WithMany(u => u.Followers)
            .HasForeignKey(uf => uf.FollowingId);
    }
}