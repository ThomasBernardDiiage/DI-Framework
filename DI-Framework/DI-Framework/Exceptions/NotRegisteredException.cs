namespace DI_Framework.Exceptions;

/// <summary>
/// Exception throw when a not registered service is called
/// </summary>
public class NotRegisteredException : Exception
{
    public NotRegisteredException(string message):base(message)
    {
    }
}