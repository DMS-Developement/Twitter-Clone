using Twitter_Clone.Models;
using Twitter_Clone.Models.RegularDTOs;

namespace Twitter_Clone.Services;

public class TweetMapper
{
    private readonly UserMapper _userMapper;

    public TweetMapper(UserMapper userMapper)
    {
        _userMapper = userMapper;
    }

    public TweetDto MapTweetToTweetDto(Tweet tweet)
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
}