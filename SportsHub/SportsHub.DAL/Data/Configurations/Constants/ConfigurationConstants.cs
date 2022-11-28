namespace SportsHub.DAL.Data.Configurations.Constants
{
    public static class ConfigurationConstants
    {

        public static class SportConstants
        {
            public const int SportNameMaxLength = 50;
            public const int SportDescriptionMaxLength = 250;
            public const string SportNameIsRequired = "Sport name is required.";
            public const string SportNameMaxLengthMessage = "The maximum length of Name is {0} characters";
            public const string SportDescriptionMaxLengthMessage = "The maximum length of Description is {0} characters";

        }

        public static class LeagueConstants
        {
            public const int LeagueNameMaxLength = 50;
            public const int LeagueDescriptionMaxLength = 250;
        }

        public static class SubLeagueConstants
        {
            public const int SubLeagueNameMaxLength = 50;
            public const int SubLeagueDescriptionMaxLength = 250;
        }

        public static class TeamConstants
        {
            public const int TeamNameMaxLength = 50;
            public const int TeamDescriptionMaxLength = 250;
        }

        public static class ArticleConstants
        {
            public const int ArticleTitleMaxLength = 100;
        }

        public static class CategoryConstants
        {
            public const int CategoryNameMaxLength = 50;
            public const int CategoryDescriptionMaxLength = 250;
        }

        public static class CommentConstants
        {
            public const int CommentContentMaxLength = 450;
        }

        public static class RoleConstants
        {
            public const int RoleNameMaxLength = 25;
        }

        public static class StateConstants
        {
            public const int StateNameMaxLenth = 30;
        }

        public static class UserConstants
        {
            public const int UserEmailMaxLenth = 50;
            public const int UserUsernameMaxLenth = 50;
            public const int UserDisplayNameMaxLenth = 50;
            public const int UserFirstNamelMaxLenth = 50;
            public const int UserLastNamelMaxLenth = 50;
            public const int UserPasswordlMaxLenth = 75;
        }
    }
}
