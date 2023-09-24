using Microsoft.EntityFrameworkCore;
using Twitter_Clone.Data;
using Twitter_Clone.Interfaces;
using Twitter_Clone.Models;
using Twitter_Clone.Models.RegularDTOs;
using Twitter_Clone.Services;

namespace Twitter_Clone.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TwitterCloneDb _context;
    private readonly UserMapper _userMapper;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(TwitterCloneDb context, UserMapper userMapper, ILogger<UserRepository> logger)
    {
        _context = context;
        _userMapper = userMapper;
        _logger = logger;
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        try
        {
            var users = await _context.Users.ToListAsync();
            List<UserDto> userDtos = new();

            foreach (var user in users) userDtos.Add(_userMapper.MapUserToDto(user));

            return userDtos;
        }
        catch (Exception e)
        {
            _logger.LogError($"Generic Error: {e.Message}");
            throw;
        }
    }

    public async Task<UserDto> GetUserProfile(int id)
    {
        try
        {
            var user = await _context.Users.Include(u => u.Following).SingleOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;

            var userDto = _userMapper.MapUserToDto(user);
            return userDto;
        }
        catch (Exception e)
        {
            _logger.LogError($"Generic Error: {e.Message}");
            throw;
        }
    }

    public async Task FollowUser(int userId, int targetUserId)
    {
        try
        {
            var userFollow = new UserFollow { FollowerId = userId, FollowingId = targetUserId };
            _context.Add(userFollow);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError($"Generic Error: {e.Message}");
            throw;
        }
    }

    public async Task UnfollowUser(int userId, int targetUserId)
    {
        try
        {
            var userFollow = await _context.UserFollows
                .Where(uf => uf.FollowerId == userId && uf.FollowingId == targetUserId)
                .FirstOrDefaultAsync();
            if (userFollow != null)
            {
                _context.Remove(userFollow);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            _logger.LogError($"Generic Error: {e.Message}");
            throw;
        }
    }
}