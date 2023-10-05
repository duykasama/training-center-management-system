namespace FAMS.V0.Shared.Exceptions;

public class UserDoesNotExistException : Exception
{
    public override string Message => "User does not exist";
}