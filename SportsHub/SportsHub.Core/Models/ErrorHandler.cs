using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SportsHub.Domain.Models;

public class ErrorHandler
{
    public int StatusCode { get; set; }
    
    public string Message { get; set; }
    
    public override string ToString() => JsonConvert.SerializeObject(this);
    
}