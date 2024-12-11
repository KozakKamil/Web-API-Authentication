using Web_API_Authentication.Model;
using Web_API_Authentication.Repositories;

namespace Web_API_Authentication.Services;

public class GameService : IGameService
{
    public Game Create(Game game)
    {
        game.Id = GameRepository.Games.Count + 1;
        GameRepository.Games.Add(game);
        return game;
    }

    public List<Game> List()
    {
        var games = GameRepository.Games;
        return games;
    }

    public Game Update(Game game)
    {
        var oldGame = GameRepository.Games.FirstOrDefault(o => o.Id == game.Id) ?? throw new Exception("Game not found");
        oldGame.Name = game.Name;
        oldGame.Description = game.Description;
        oldGame.Platform = game.Platform;
        oldGame.Publisher = game.Publisher;
        oldGame.Developer = game.Developer;
        oldGame.ReleaseDate = game.ReleaseDate;

        return game;
    }

    public bool Delete(int id)
    {
        var game = GameRepository.Games.FirstOrDefault(o => o.Id == id);
        if (game is null)
        {
            return false;
        }

        GameRepository.Games.Remove(game);
        return true;
    }

    public Game? Get(int id)
    {
        var game = GameRepository.Games.FirstOrDefault(o => o.Id == id);
        return game is null ? null : game;
    }
}
