using AutoMapper;
using PatPortal.Application.DTOs.Request.Posts;
using PatPortal.Application.DTOs.Request.Users;
using PatPortal.Application.DTOs.Response.Comments;
using PatPortal.Application.DTOs.Response.Posts;
using PatPortal.Application.DTOs.Response.Users;
using PatPortal.Domain.Entities.Comments;
using PatPortal.Domain.Entities.Posts;
using PatPortal.Domain.Entities.Posts.Requests;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Entities.Users.Requests;
using PatPortal.Domain.ValueObjects;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Application.Mappers
{
    public class DtoMapperProfile : Profile
    {
        public DtoMapperProfile()
        {
            CreateMapsForUser();
            CreateMapsForPosts();
            CreateMappComments();
        }

        private void CreateMapsForUser()
        {
            CreateMap<User, UserForViewDto>();

            CreateMap<UserForCreationDto, UserCreate>()
                .ForCtorParam("email", src => src.MapFrom(s => new Email(s.Email)))
                .ForCtorParam("dayOfBirht", src => src.MapFrom(s => s.DayOfBirht.ParseToDateTime()))
                .ForCtorParam("id", src => src.MapFrom(s => Guid.NewGuid()));

            CreateMap<UserForUpdateDto, UserUpdate>()
                .ForCtorParam("email", src => src.MapFrom(s => new Email(s.Email)))
                .ForCtorParam("dayOfBirht", src => src.MapFrom(s => s.DayOfBirht.ParseToDateTime()));
        }
        private void CreateMapsForPosts()
        {
            CreateMap<PostForCreationDto, PostCreate>()
                .ForCtorParam("ownerId", src => src.MapFrom(s => Guid.Parse(s.OwnerId)));

            CreateMap<PostForUpdateDto, PostUpdate>()
                .ForCtorParam("ownerId", src => src.MapFrom(s => Guid.Parse(s.OwnerId)))
                .ForCtorParam("id", src => src.MapFrom(s => Guid.Parse(s.Id)));

            CreateMap<Post, PostForViewDto>()
                .ForMember(dest => dest.OwnerId, src => src.MapFrom(s => s.Owner.Id.ToString()));               
        }

        private void CreateMappComments()
        {
            CreateMap<Comment, CommentForViewDto>()
                .ForMember(dest => dest.OwnerId, src => src.MapFrom(s => s.Owner.Id.ToString()))
                .ForMember(dest => dest.PostId, src => src.MapFrom(s => s.Post.Id));
                
        }
    }
}
