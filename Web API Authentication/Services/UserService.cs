using Web_API_Authentication.Model;
using Web_API_Authentication.Repositories;

namespace Web_API_Authentication.Services;

public class UserService : IUserInterface
{
    public User Get(UserLogin userLogin)
    {
        User ?user = UserRepositories.Users.FirstOrDefault(o => o.UserName.Equals
        (userLogin.UserName, StringComparison.OrdinalIgnoreCase) && o.Password.Equals(userLogin.Password));

        return user;
    }
}
