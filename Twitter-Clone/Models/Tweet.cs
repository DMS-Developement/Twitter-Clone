namespace Twitter_Clone.Models;

public class Tweet
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public User User { get; set; } = null!;
}