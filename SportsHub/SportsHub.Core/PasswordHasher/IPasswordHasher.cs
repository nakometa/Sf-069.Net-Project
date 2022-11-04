namespace SportsHub.Domain.PasswordHasher
{
    public interface IPasswordHasher
    {
        string Hash(string password);

        IPasswordCheckResult Check(string hash, string password);
    }
}
