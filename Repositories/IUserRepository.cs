using TEAM_ONE_AND_ZERO_BACKEND.Models;

namespace TEAM_ONE_AND_ZERO_BACKEND.Repositories;

public interface IUserRepository
{
    User CreateUser (User user);
    string SignIn (string username, string password);
    User GetUserById(int user);
    IEnumerable<User> GetAllUsers ();
    Task<User?> GetUserByUsername (string username);
    User GetCurrentUser ();
}