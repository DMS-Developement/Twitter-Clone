using Twitter_Clone.Models;

namespace Twitter_Clone.Interfaces;

public interface ITweetRepository
{
    public Task<Tweet> CreateTweet(Tweet tweet);
    public Task<Tweet> GetTweetById(int id);
    public Task<IEnumerable<Tweet>> GetAllTweets();
    public Task<IEnumerable<Tweet>> GetTweetsByUserId(int userId);
    Task DeleteTweet(int id);
}