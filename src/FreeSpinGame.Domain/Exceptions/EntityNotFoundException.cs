namespace FreeSpinGame.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityType, string entityId) : base($"Entity {entityType} with id {entityId} want not found") {}
}