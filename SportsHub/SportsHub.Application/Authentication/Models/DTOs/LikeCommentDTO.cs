namespace SportsHub.AppService.Authentication.Models.DTOs
{
    public class LikeCommentDTO
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public bool IsLike { get; set; }
    }
}
