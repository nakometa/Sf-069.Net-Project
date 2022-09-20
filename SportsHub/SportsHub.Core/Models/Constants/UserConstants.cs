using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.Domain.Models.Constants
{
    public class UserConstants
    {
        public static List<User> Users = new List<User>()
        {
            new User()
            {
                Id = 1, 
                Email = "atanaskirilov@gmail.com",
                Username = "nakometa", 
                FirstName = "Atanas", 
                LastName = "Kirilov",
                DisplayName = "Atanas Kirilov", 
                Password = "testpassword123", 
                ProfilePicture = new byte[]{1,2,3}, 
                Role = new Role(){Id = 0, Name = "User", Users = new List<User>()}
            },
            new User()
            {
                Id = 2, 
                Email = "stamopetkov@gmail.com", 
                Username = "stamopetkov", 
                FirstName = "Stamo", 
                LastName = "Petkov",
                DisplayName = "Stamo Petkov", 
                Password = "testpassword321", 
                ProfilePicture = new byte[]{1,2,3}, 
                Role = new Role(){Id = 1, Name = "Admin", Users = new List<User>()}
            }
        };
    }
}
