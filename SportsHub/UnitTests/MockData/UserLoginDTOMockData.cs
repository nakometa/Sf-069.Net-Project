using SportsHub.AppService.Authentication.Models.DTOs;

namespace UnitTests.MockData;

public class UserLoginDTOMockData
{
    public static UserLoginDTO GetUserWithUsername()
    {
        return new UserLoginDTO()
        {
           UsernameOrEmail = "gogo",
           Password = "testpassword123"
        };
    }

    public static UserLoginDTO GetUserWithEmail()
    {
        return new UserLoginDTO()
        {
            UsernameOrEmail = "goshko88@mail.bg",
            Password = "testpassword123"
        };
    }
}