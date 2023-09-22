using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twitter_Clone.Interfaces;

namespace Twitter_Clone.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users);
        }
        catch (Exception e)
        {
            _logger.LogError($"Something went wrong: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Something went wrong" });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserProfile(int id)
    {
        try
        {
            var user = await _userRepository.GetUserProfile(id);
            if (user == null) return NotFound("User not found!");
            return Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogError($"Something went wrong: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Something went wrong" });
        }
    }

    [HttpPost("{userId}/follow")]
    public async Task<IActionResult> FollowUser(int userId, [FromBody] int targetUserId)
    {
        try
        {
            await _userRepository.FollowUser(userId, targetUserId);
            return Ok(new { message = "User followed successfully!" });
        }
        catch (Exception e)
        {
            _logger.LogError($"Something went wrong: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Something went wrong" });
        }
    }

    [HttpPost("{userId}/unfollow")]
    public async Task<IActionResult> UnfollowUser(int userId, [FromBody] int targetUserId)
    {
        try
        {
            await _userRepository.UnfollowUser(userId, targetUserId);
            return Ok(new { message = "User unfollowed successfully!" });
        }
        catch (Exception e)
        {
            _logger.LogError($"Something went wrong: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Something went wrong" });
        }
    }
}