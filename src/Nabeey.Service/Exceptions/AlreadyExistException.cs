namespace Nabeey.Service.Exceptions;

public class AlreadyExistException : Exception
{
	public AlreadyExistException(string message) : base(message)
	{ }

	public AlreadyExistException(string message, Exception innerException) : base(message, innerException)
	{ }

	public int StatusCode { get; set; } = 403;
}
