using AutoMapper;
using SportsHub.Api.DTOs;
using SportsHub.Domain.Models;

namespace SportsHub.Api.Profiles;

/// <summary>
/// Class <c>UserProfile</c> acts as a mapping profile for the <c>User</c> entity.
/// </summary>

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDTO>().ReverseMap();
    }
}
