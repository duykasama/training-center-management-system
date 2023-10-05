namespace FAMS.V0.Shared.Exceptions;

public class UserAlreadyExistsException : System.Exception
{
    public override string Message => "User already exists";
}