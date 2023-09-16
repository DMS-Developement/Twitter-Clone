using Microsoft.EntityFrameworkCore;
using Twitter_Clone.Data;
using Twitter_Clone.Interfaces;
using Twitter_Clone.Models;

namespace Twitter_Clone.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TwitterCloneDb _context;

    public UserRepository(TwitterCloneDb context)
    {
        _context = context;
    }

    public async Task<User> GetUserProfile(int id)
    {
        return await _context.Users.FindAsync(id);
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