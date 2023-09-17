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
    private readonly ILogger<TweetController> _logger;

    public TweetController(ITweetRepository tweetRepository, ILogger<TweetController> logger)
    {
        _tweetRepository = tweetRepository;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTweet([FromBody] Tweet tweet)
    {
        try
        {
            var createdTweet = await _tweetRepository.CreateTweet(tweet);
            return Ok(createdTweet);
        }
        catch (UnauthorizedAccessException e)
        {
            _logger.LogError($"Unauthorized: {e}");
            return Unauthorized(new { Message = "You are not authorized to create a tweet" });
        }
        catch (ArgumentException e)
        {
            _logger.LogError($"Something went wrong: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Something was wrong with your submission" });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTweetById(int id)
    {
        try
        {
            var tweet = await _tweetRepository.GetTweetById(id);
            if (tweet == null) return NotFound("Tweet not found");
            return Ok(tweet);
        }
        catch (Exception e)
        {
            _logger.LogError($"Something went wrong: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Something went wrong" });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTweets()
    {
        try
        {
            var tweets = await _tweetRepository.GetAllTweets();
            return Ok(tweets);
        }
        catch (Exception e)
        {
            _logger.LogError($"Something went wrong: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Something went wrong" });
        }
    }

    [HttpGet("user/{userId}/tweets")]
    public async Task<IActionResult> GetTweetsByUserId(int userId)
    {
        try
        {
            var tweets = await _tweetRepository.GetTweetsByUserId(userId);
            return Ok(tweets);
        }
        catch (Exception e)
        {
            _logger.LogError($"Something went wrong: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Something went wrong" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTweet(int id)
    {
        try
        {
            await _tweetRepository.DeleteTweet(id);
            return Ok(new { Message = "Tweet successfully deleted" });
        }
        catch (Exception e)
        {
            _logger.LogError($"Something went wrong: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Something went wrong" });
        }
    }
}