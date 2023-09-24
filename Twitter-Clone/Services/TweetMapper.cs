using Twitter_Clone.Data;
using Twitter_Clone.Models;
using Twitter_Clone.Models.RegularDTOs;

namespace Twitter_Clone.Services;

public class TweetMapper
{
    private readonly UserMapper _userMapper;
    private readonly ILogger<TweetMapper> _logger;
    private readonly TwitterCloneDb _context;

    public TweetMapper(UserMapper userMapper, ILogger<TweetMapper> logger, TwitterCloneDb context)
    {
        _userMapper = userMapper;
        _logger = logger;
        _context = context;
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
            };
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError($"InvalidOperationException: {e.Message}");
            throw;
        }
    }

    public Tweet MapTweetDtoToTweet(TweetDto tweetDto)
    {
        try
        {
            return new Tweet
            {
                Id = tweetDto.Id,
                Content = tweetDto.Content,
                UserId = tweetDto.UserId,
                CreatedAt = tweetDto.CreatedAt,
                User = _context.Users.Find(tweetDto.UserId)
            };
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError($"InvalidOperationException: {e.Message}");
            throw;
        }
    }

}