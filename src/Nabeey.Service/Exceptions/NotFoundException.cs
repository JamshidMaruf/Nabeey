namespace Nabeey.Service.Exceptions;

public class NotFoundException : Exception
{
	public NotFoundException(string message) : base(message)
	{ }

	public NotFoundException(string message, Exception innerException) : base(message, innerException)
	{ }

	public int StatusCode { get; set; } = 404;
}
