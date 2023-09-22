namespace Twitter_Clone.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? ProfileImagePath { get; set; }
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime LastLogin { get; set; }
    public ICollection<Tweet>? Tweets { get; set; }
    public ICollection<UserFollow>? Followers { get; set; }
    public ICollection<UserFollow>? Following { get; set; }
}