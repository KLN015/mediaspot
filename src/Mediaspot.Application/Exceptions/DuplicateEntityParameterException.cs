namespace Mediaspot.Application.Exceptions;

public class DuplicateEntityParameterException: Exception
{
    public DuplicateEntityParameterException(string name) : base($"An enity with parameter '{name}' already exists.")
    {
        
    }
}