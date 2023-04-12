using HeyListen.Database.Models;
using MongoDB.Driver;

namespace HeyListen.Database;

public class GameRepository : BaseRepository<Game>
{
    public GameRepository(IMongoClient mongoClient) : base(mongoClient)
    {
        CollectionName = "games";
    }
}