using Twitter_Clone.Models.RegularDTOs;

namespace Twitter_Clone.Interfaces;

public interface IUserRepository
{
    Task<UserDto> GetUserProfile(int id);
    Task<List<UserDto>> GetAllUsers();
    Task FollowUser(int userId, int targetUserId);
    Task UnfollowUser(int userId, int targetUserId);
}