using Microsoft.EntityFrameworkCore;
using Twitter_Clone.Data;
using Twitter_Clone.Interfaces;
using Twitter_Clone.Models;

namespace Twitter_Clone.Repositories;

public class TweetRepository : ITweetRepository
{
    private readonly TwitterCloneDb _context;

    public TweetRepository(TwitterCloneDb context)
    {
        _context = context;
    }

    public async Task<Tweet> CreateTweet(Tweet tweet)
    {
        _context.Add(tweet);
        await _context.SaveChangesAsync();
        return tweet;
    }

    public async Task<Tweet> GetTweetById(int id)
    {
        return await _context.Tweets.FindAsync(id);
    }

    public async Task<IEnumerable<Tweet>> GetAllTweets()
    {
        return await _context.Tweets.ToListAsync();
    }

    public async Task<IEnumerable<Tweet>> GetTweetsByUserId(int userId)
    {
        return await _context.Tweets.Where(t => t.UserId == userId).ToListAsync();
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