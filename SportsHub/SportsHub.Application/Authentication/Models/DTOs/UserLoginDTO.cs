namespace SportsHub.AppService.Authentication.Models.DTOs
{
    public class UserLoginDTO
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
