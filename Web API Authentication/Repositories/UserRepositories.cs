using Web_API_Authentication.Model;

namespace Web_API_Authentication.Repositories;

public class UserRepositories
{
    public static List<User> Users = new List<User>
    {
        new User
        {
            UserName = "admin",
            EmailAddress = "admin@localhost",
            Password = "admin",
            GivenName = "Admin",
            SurName = "Admin",
            Role = "Admin"
        },
        new User
        {
            UserName = "user",
            EmailAddress = "user@localhost",
            Password = "user",
            GivenName = "User",
            SurName = "User",
            Role = "User"
        }
    };  
}
