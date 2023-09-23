namespace Twitter_Clone.Models.RegularDTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? ProfileImagePath { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastLogin { get; set; }
    public ICollection<UserFollow>? Followers { get; set; }
    public ICollection<UserFollow>? Following { get; set; }
}