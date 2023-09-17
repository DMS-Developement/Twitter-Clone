using Microsoft.EntityFrameworkCore;
using Twitter_Clone.Data;
using Twitter_Clone.Interfaces;
using Twitter_Clone.Models;
using Twitter_Clone.Models.RegularDTOs;
using Twitter_Clone.Services;

namespace Twitter_Clone.Repositories;

public class TweetRepository : ITweetRepository
{
    private readonly TwitterCloneDb _context;
    private readonly UserMapper _userMapper;
    private readonly TweetMapper _tweetMapper;

    public TweetRepository(TwitterCloneDb context, UserMapper userMapper, TweetMapper tweetMapper)
    {
        _context = context;
        _userMapper = userMapper;
        _tweetMapper = tweetMapper;
    }

    public async Task<TweetDto> CreateTweet(Tweet tweet)
    {
        var user = await _context.Users.FindAsync(tweet.UserId);
        if (user == null) throw new Exception("User does not exist");

        tweet.User = user;

        _context.Tweets.Add(tweet);
        await _context.SaveChangesAsync();

        return _tweetMapper.MapTweetToTweetDto(tweet);
    }

    public async Task<TweetDto> GetTweetById(int id)
    {
        var tweet = await _context.Tweets.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);

        if (tweet == null) return null;

        return _tweetMapper.MapTweetToTweetDto(tweet);
    }

    public async Task<List<TweetDto>> GetAllTweets()
    {
        var tweets = await _context.Tweets.Include(t => t.User).ToListAsync();

        List<TweetDto> tweetDtos = new();

        foreach (var tweet in tweets) tweetDtos.Add(_tweetMapper.MapTweetToTweetDto(tweet));

        return tweetDtos;
    }

    public async Task<List<TweetDto>> GetTweetsByUserId(int userId)
    {
        var tweets = await _context.Tweets.Where(t => t.UserId == userId).Include(t => t.User).ToListAsync();

        List<TweetDto> tweetDtos = new();

        foreach (var tweet in tweets) tweetDtos.Add(_tweetMapper.MapTweetToTweetDto(tweet));

        return tweetDtos;
    }

    public async Task DeleteTweet(int id)
    {
        var tweet = await _context.Tweets.FindAsync(id);
        if (tweet != null)
        {
            _context.Tweets.Remove(tweet);
            await _context.SaveChangesAsync();
        }
    }
}