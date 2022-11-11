namespace DI_Framework.Exceptions;

public class TooManyConstructorException : Exception
{
    public TooManyConstructorException(string message) : base(message)
    {
    }
}