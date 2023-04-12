namespace HeyListen.Database.Models;

public class Game : Resource
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Developer { get; set; }
    public string Publisher { get; set; }
    public List<ReleaseDate> ReleaseDates { get; set; }
}

public class ReleaseDate
{
    public string Region { get; set; }
    public DateOnly Date { get; set; }
}