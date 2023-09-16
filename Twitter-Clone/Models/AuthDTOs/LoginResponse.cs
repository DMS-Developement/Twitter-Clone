namespace Twitter_Clone.Models.AuthDTOs;

public class LoginResponse
{
    public string Username { get; set; } = null!;
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
}