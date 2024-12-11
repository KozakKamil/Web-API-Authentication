using Web_API_Authentication.Model;

namespace Web_API_Authentication.Services;

public interface IUserInterface
{
    public User Get(UserLogin userLogin);
}
