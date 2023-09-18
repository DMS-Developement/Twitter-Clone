using Twitter_Clone.Models;
using Twitter_Clone.Models.RegularDTOs;

namespace Twitter_Clone.Services;

public class UserMapper
{
    private readonly ILogger<UserMapper> _logger;

    public UserMapper(ILogger<UserMapper> logger)
    {
        _logger = logger;
    }
    public UserDto MapUserToDto(User user)
    {
        try
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                LastLogin = user.LastLogin,
                Followers = user.Followers,
                Following = user.Following
            };
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError($"InvalidOperationException: {e.Message}");
            throw;
        }
    }

}