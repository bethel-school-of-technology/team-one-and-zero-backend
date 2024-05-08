using team_one_and_zero.Models;

namespace team_one_and_zero.Repositories;

public interface IUserRepository
{
    User CreateUser (User user);
    string SignIn (string username, string password);

}