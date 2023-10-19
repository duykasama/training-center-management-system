namespace FAMS.V0.Shared.Exceptions;

public class MissingSystemPermissionsException : Exception
{
    public override string Message => "User permission not found, permissions could have not been initialized";
}