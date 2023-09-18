using Twitter_Clone.Models;
using Twitter_Clone.Models.RegularDTOs;

namespace Twitter_Clone.Services;

public class TweetMapper
{
    private readonly UserMapper _userMapper;
    private readonly ILogger<TweetMapper> _logger;

    public TweetMapper(UserMapper userMapper, ILogger<TweetMapper> logger)
    {
        _userMapper = userMapper;
        _logger = logger;
    }

    public TweetDto MapTweetToTweetDto(Tweet tweet)
    {
        try
        {
            return new TweetDto
            {
                Id = tweet.Id,
                Content = tweet.Content,
                UserId = tweet.UserId,
                CreatedAt = tweet.CreatedAt,
                User = _userMapper.MapUserToDto(tweet.User)
            };
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError($"InvalidOperationException: {e.Message}");
            throw;
        }
    }

}