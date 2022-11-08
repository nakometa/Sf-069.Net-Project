using SportsHub.Domain.Models;

namespace UnitTests.MockData
{
    public class ArticleMockData
    {
        public static Article GetArticle()
        {
            return new Article()
            {
                Title = "testArticle"                
            };
        }
    }
}
