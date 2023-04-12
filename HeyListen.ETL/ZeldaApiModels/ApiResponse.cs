namespace HeyListen.ETL.ZeldaApiModels;

public class ApiResponse<T> where T : DataObject
{
    public bool Success { get; set; }
    public int Count { get; set; }
    public List<T> Data { get; set; }
}