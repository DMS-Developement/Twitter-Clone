using Twitter_Clone.Models;
using Twitter_Clone.Models.RegularDTOs;

namespace Twitter_Clone.Interfaces;

public interface ITweetRepository
{
    public Task<TweetDto> CreateTweet(Tweet tweet);
    public Task<TweetDto> GetTweetById(int id);
    public Task<List<TweetDto>> GetAllTweets();
    public Task<List<TweetDto>> GetTweetsByUserId(int userId);
    Task DeleteTweet(int id);
}