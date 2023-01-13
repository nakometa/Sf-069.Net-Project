using AutoMapper;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;

namespace SportsHub.Api.Mapping
{
    public class CommentMapping : Profile
    {
        public CommentMapping()
        {
            CreateMap<LikeCommentDTO, CommentLike>();
        }
    }
}
