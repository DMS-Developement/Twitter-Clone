using Twitter_Clone.Models.RegularDTOs;

namespace Twitter_Clone.Interfaces;

public interface ITweetRepository
{
    public Task<TweetDto> CreateTweet(TweetDto tweet);
    public Task<TweetDto> GetTweetById(int id);
    public Task<List<TweetDto>> GetAllTweets();
    public Task<List<TweetDto>> GetTweetsByUserId(int userId);
    public Task<List<TweetDto>> GetTweetsByFollowingUsers(int userId);
    Task DeleteTweet(int id);
}