using System.Net;

public class NotFoundException : Exception
{
    public int StatusCode { get; set; } = (int)HttpStatusCode.NotFound;

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}