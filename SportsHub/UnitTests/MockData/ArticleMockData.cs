using SportsHub.Domain.Models;

namespace UnitTests.MockData
{
    public class ArticleMockData
    {
        public static Article GetArticle()
        {
            return new Article()
            {
                Id = 5,
                Title = "testArticle"
            };
        }

        public static IEnumerable<Article> GetAll()
        {
            return new List<Article>
            {
                new Article()
                {
                Title = "testArticle1"
                },
                new Article()
                {
                Title = "testArticle2"
                },
                new Article()
                {
                Title = "testArticle3"
                },
            };
        }

        public static IEnumerable<Article> GetNone()
        {
            return new List<Article>();
        }
    }
}
