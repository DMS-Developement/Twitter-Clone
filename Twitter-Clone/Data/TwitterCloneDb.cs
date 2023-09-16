using Microsoft.EntityFrameworkCore;
using Twitter_Clone.Models;

namespace Twitter_Clone.Data;

public class TwitterCloneDb : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Tweet> Tweets { get; set; }

    public TwitterCloneDb(DbContextOptions<TwitterCloneDb> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Tweets)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);
    }
}