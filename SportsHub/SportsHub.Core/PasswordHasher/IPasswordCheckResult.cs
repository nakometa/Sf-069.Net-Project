namespace SportsHub.Domain.PasswordHasher
{
    public interface IPasswordCheckResult
    {
        public bool Verified { get; }
        public bool NeedsUpgrade { get; }
    }
}
