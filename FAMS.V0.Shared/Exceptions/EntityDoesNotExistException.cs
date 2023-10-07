namespace FAMS.V0.Shared.Exceptions;

public class EntityDoesNotExistException : Exception
{
    public override string Message => "Entity does not exist";
}