namespace Nabeey.Service.Exceptions;

public class CustomException : Exception
{
    public int StatusCode { get; set; }
    public CustomException(int StatusCode, string message) : base(message)
    {
        this.StatusCode = StatusCode;
    }
}
