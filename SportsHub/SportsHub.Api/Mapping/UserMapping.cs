using AutoMapper;
using SportsHub.Api.DTOs;
using SportsHub.Domain.Models;

namespace SportsHub.Api.Profiles;

/// <summary>
/// Class <c>UserMapping</c> acts as a mapping profile for the <c>User</c> entity.
/// </summary>

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserResponseDTO>();
    }
}
