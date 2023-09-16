using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twitter_Clone.Interfaces;
using Twitter_Clone.Models;

namespace Twitter_Clone.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TweetController : ControllerBase
{
    private readonly ITweetRepository _tweetRepository;

    public TweetController(ITweetRepository tweetRepository)
    {
        _tweetRepository = tweetRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTweet([FromBody] Tweet tweet)
    {
        var createdTweet = await _tweetRepository.CreateTweet(tweet);
        return Ok(createdTweet);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTweetById(int id)
    {
        var tweet = await _tweetRepository.GetTweetById(id);
        if (tweet == null) return NotFound("Tweet not found");
        return Ok(tweet);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTweets()
    {
        var tweets = await _tweetRepository.GetAllTweets();
        return Ok(tweets);
    }

    [HttpGet("user/{userId}/tweets")]
    public async Task<IActionResult> GetTweetsByUserId(int userId)
    {
        var tweets = await _tweetRepository.GetTweetsByUserId(userId);
        return Ok(tweets);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTweet(int id)
    {
        await _tweetRepository.DeleteTweet(id);
        return Ok(new { Message = "Tweet successfully deleted" });
    }
}