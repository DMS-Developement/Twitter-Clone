using Twitter_Clone.Data;
using Twitter_Clone.Models;
using Twitter_Clone.Models.RegularDTOs;

namespace Twitter_Clone.Services;

public class UserMapper
{
    private readonly ILogger<UserMapper> _logger;
    private readonly TwitterCloneDb _context;

    public UserMapper(ILogger<UserMapper> logger, TwitterCloneDb context)
    {
        _logger = logger;
        _context = context;
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
                ProfileImagePath = user.ProfileImagePath,
                CreatedAt = user.CreatedAt,
                LastLogin = user.LastLogin,
                Following = user.Following?.Select(follow => new UserFollowDto { FollowerId = follow.FollowerId, FollowingId = follow.FollowingId }).ToList(),
                Followers = user.Followers?.Select(follow => new UserFollowDto { FollowerId = follow.FollowerId, FollowingId = follow.FollowingId }).ToList()
            };
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError($"InvalidOperationException: {e.Message}");
            throw;
        }
    }
}