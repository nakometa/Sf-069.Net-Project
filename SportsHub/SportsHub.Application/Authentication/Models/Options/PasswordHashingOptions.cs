namespace SportsHub.AppService.Authentication.Models.Options
{
    public sealed class PasswordHashingOptions
    {
        public int Iterations { get; } = 10000;
    }
}
