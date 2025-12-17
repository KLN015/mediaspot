namespace Mediaspot.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Guid id) : base($"Entity with id '{id}' was not found.")
    {
        
    }
}