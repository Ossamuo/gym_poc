using System.Text.Json.Serialization;

namespace Gym.Domain.Contexts.SharedContext.Results;

public class Result<TData>
{
        
    private int _code;

     
    [JsonConstructor]
    //parameterless constructor
    public Result()
    {
        _code = Configuration.DefaultStatusCode;
    }
    public Result(TData? data, 
        int code = Configuration.DefaultStatusCode, 
        string? message = null)
    {
        Data = data;
        Message = message;
        _code = code;

    }
    public TData? Data { get; set; }
    public string? Message { get; set; }

    [JsonIgnore]
    public bool IsSuccess => _code is >= 200 and <= 299;
}