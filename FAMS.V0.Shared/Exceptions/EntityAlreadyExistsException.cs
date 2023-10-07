namespace FAMS.V0.Shared.Exceptions;

public class EntityAlreadyExistsException : Exception
{
    public override string Message => "Entity already exists";
}