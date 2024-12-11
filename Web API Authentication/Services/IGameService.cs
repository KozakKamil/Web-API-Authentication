using Web_API_Authentication.Model;

namespace Web_API_Authentication.Services;

public interface IGameService
{
    public Game Create(Game game);
    public Game Get(int id);
    public List<Game> List();
    public Game Update(Game game);
    public bool Delete(int id);
}
