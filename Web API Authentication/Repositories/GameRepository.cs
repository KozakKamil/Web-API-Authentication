using Web_API_Authentication.Model;

namespace Web_API_Authentication.Repositories;

public class GameRepository
{
    public static List<Game> Games = new List<Game>
    {
        new Game
        {
            Id = 1,
            Name = "The Legend of Zelda: Breath of the Wild",
            Description = "The Legend of Zelda: Breath of the Wild is an action-adventure game developed and published by Nintendo, released for the Nintendo Switch and Wii U consoles on March 3, 2017.",
            Platform = "Nintendo Switch, Wii U",
            Publisher = "Nintendo",
            Developer = "Nintendo EPD",
            ReleaseDate = "March 3, 2017"
        },
        new Game
        {
            Id = 2,
            Name = "The Witcher 3: Wild Hunt",
            Description = "The Witcher 3: Wild Hunt is a 2015 action role-playing game developed and published by CD Projekt. Based on The Witcher series of fantasy novels by Polish author Andrzej Sapkowski, it is the sequel to the 2011 game The Witcher 2: Assassins of Kings.",
            Platform = "Microsoft Windows, PlayStation 4, Xbox One, Nintendo Switch",
            Publisher = "CD Projekt",
            Developer = "CD Projekt Red",
            ReleaseDate = "May 19, 2015"
        },
        new Game
        {  
            Id = 3,
            Name = "Red Dead Redemption 2",
            Description = "Red Dead Redemption 2 is a 2018 action-adventure game developed and published by Rockstar Games. The game is the third entry in the Red Dead series and is a prequel to the 2010 game Red Dead Redemption.",
            Platform = "Microsoft Windows, PlayStation 4, Xbox One, Google Stadia",
            Publisher = "Rockstar Games",
            Developer = "Rockstar Studios",
            ReleaseDate = "October 26, 2018"
        }
    };
}
