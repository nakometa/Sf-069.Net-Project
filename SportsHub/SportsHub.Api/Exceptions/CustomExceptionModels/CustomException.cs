namespace SportsHub.Api.Exceptions.CustomExceptionModels;

public abstract class CustomException: Exception
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public abstract override string ToString();
}