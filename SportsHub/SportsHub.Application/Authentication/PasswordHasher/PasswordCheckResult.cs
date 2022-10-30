using SportsHub.Domain.PasswordHasher;

namespace SportsHub.AppService.Authentication.PasswordHasher
{
    public class PasswordCheckResult : IPasswordCheckResult
    {
        public PasswordCheckResult(bool verified, bool needsUpgrade)
        {
            Verified = verified;
            NeedsUpgrade = needsUpgrade;
        }

        public bool Verified { get;}
        public bool NeedsUpgrade { get;}
    }
}
