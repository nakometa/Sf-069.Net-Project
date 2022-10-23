using SportsHub.Domain.Models;

namespace UnitTests.MockData;

public class UserMockData
{
    public static User GetUser()
    {
        return new User
        {
            Id = 8, 
            Email = "goshko88@mail.bg",
            Username = "gogo", 
            FirstName = "Georgi", 
            LastName = "Ivanov",
            DisplayName = "Georgi Ivanov", 
            Password = "testpassword123", 
            ProfilePicture = new byte[]{1,2,3}, 
            Role =  new Role(){Id = 6, Name = "User", Users = new List<User>()}
        };
    }
}