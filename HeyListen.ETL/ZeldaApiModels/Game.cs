using System.Text.Json.Serialization;

namespace HeyListen.ETL.ZeldaApiModels;

public class Game : DataObject
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Developer { get; set; }
    public string Publisher { get; set; }
    public string ReleasedDate { get; set; }
}