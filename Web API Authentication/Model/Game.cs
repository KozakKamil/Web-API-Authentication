namespace Web_API_Authentication.Model;

public class Game
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public  string? Description { get; set; }
    public  string? Platform { get; set; }
    public  string? Publisher { get; set; }
    public  string? Developer { get; set; }
    public  string? ReleaseDate { get; set; }
}
