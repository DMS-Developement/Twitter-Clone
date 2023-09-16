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

    public UserRepository(TwitterCloneDb context, UserMapper userMapper)
    {
        _context = context;
        _userMapper = userMapper;
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await _context.Users.ToListAsync();
        List<UserDto> userDtos = new();

        foreach (var user in users) userDtos.Add(_userMapper.MapUserToDto(user));

        return userDtos;
    }

    public async Task<UserDto> GetUserProfile(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null) return null;

        var userDto = _userMapper.MapUserToDto(user);
        return userDto;
    }

    public async Task FollowUser(int userId, int targetUserId)
    {
        var userFollow = new UserFollow { FollowerId = userId, FollowingId = targetUserId };
        _context.Add(userFollow);
        await _context.SaveChangesAsync();
    }

    public async Task UnfollowUser(int userId, int targetUserId)
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
}