namespace Twitter_Clone.Models.RegularDTOs;

public class TweetDto
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}