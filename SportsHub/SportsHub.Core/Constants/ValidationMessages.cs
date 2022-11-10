namespace SportsHub.Domain.Constants
{
    public static class ValidationMessages
    {
        public const string InvalidLogin = "Invalid username or password";
        public const string RegisterSuccessful = "Register successful";
        public const string RegisterNotSuccessful = "Username or Email is already in use";
        public const string Public = "Public property";
        public const string UserNotFound = "User not found";
        public const string UserFound = "User: {0}";
        public const string AdminEndpoint = "Hi, {0}, you are an Admin";
        public const string NoArticles = "No articles in the database";
        public const string NoSuchArticle = "No such article";
        public const string NoCommentsForArticle = "No comments for this article";
        public const string UnableToPostComment = "Unable to post comment";
        public const string CommentPostedSuccessfully = "Comment posted successfully";
    }
}
