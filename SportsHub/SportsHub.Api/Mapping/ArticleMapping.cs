using AutoMapper;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;

namespace SportsHub.Api.Mapping
{
    public class ArticleMapping : Profile
    {
        public ArticleMapping()
        {
            CreateMap<Article, ArticleResponseDTO>();
        }
    }
}
