namespace SportsHub.AppService.Authentication.Models.Options
{
    public sealed class PasswordHashingOptions
    {
        public int Iterations { get; set; } = 10000;
    }
}
