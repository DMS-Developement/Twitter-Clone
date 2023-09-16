using Microsoft.AspNetCore.Mvc;
using Twitter_Clone.Interfaces;

namespace Twitter_Clone.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserProfile(int id)
    {
        var user = await _userRepository.GetUserProfile(id);
        if (user == null) return NotFound("User not found!");
        return Ok(user);
    }

    [HttpPost("{userId}/follow")]
    public async Task<IActionResult> FollowUser(int userId, [FromBody] int targetUserId)
    {
        await _userRepository.FollowUser(userId, targetUserId);
        return Ok(new { message = "User followed successfully!" });
    }

    [HttpPost("{userId}/unfollow")]
    public async Task<IActionResult> UnfollowUser(int userId, [FromBody] int targetUserId)
    {
        await _userRepository.UnfollowUser(userId, targetUserId);
        return Ok(new { message = "User unfollowed successfully!" });
    }
}