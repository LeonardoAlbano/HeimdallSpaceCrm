namespace HeimdallSpaceCrm.Exception;

public class EmailAlreadyExistsException : DomainException
{
    public EmailAlreadyExistsException(string email)
        : base($"Email '{email}' já está em uso.")
    {
    }
}