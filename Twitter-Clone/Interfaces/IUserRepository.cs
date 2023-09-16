using Twitter_Clone.Models;

namespace Twitter_Clone.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserProfile(int id);
    Task FollowUser(int userId, int targetUserId);
    Task UnfollowUser(int userId, int targetUserId);
}