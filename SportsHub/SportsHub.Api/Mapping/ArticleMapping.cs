using AutoMapper;
using SportsHub.Api.Mapping.Models;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;

namespace SportsHub.Api.Mapping
{
    public class ArticleMapping : Profile
    {
        public ArticleMapping()
        {
            CreateMap<Article, ArticleResponseDTO>()
                .ForMember(dest => dest.StateName, act => act.MapFrom(a => a.State.Name))
                .ForMember(dest => dest.CategoryName, act => act.MapFrom(a => a.Category.Name));

            CreateMap<CreateArticleDTO, Article>()
                .ForMember(dest => dest.Title, act => act.Ignore());
        }
    }
}
