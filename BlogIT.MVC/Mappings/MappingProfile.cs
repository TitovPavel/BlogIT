using AutoMapper;
using BlogIT.DB.Models;
using BlogIT.MVC.ViewModels;
using System;
using System.Linq;

namespace BlogIT.MVC.Mappings
{
    public class MappingProfile: Profile
    {

        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>().ReverseMap();
            CreateMap<CreateUserViewModel, User>().ReverseMap();
            CreateMap<EditUserViewModel, User>().ReverseMap();
            CreateMap<User, UserViewModel>()
                .ForMember(d => d.IsLocked, o => o.MapFrom(s => (s.LockoutEnabled && s.LockoutEnd > DateTime.Now)));
            CreateMap<ProfileSettingsViewModel, User>();
            CreateMap<User, ProfileSettingsViewModel>()
                .ForMember(d => d.AvatarExist, o => o.MapFrom(s => (s.AvatarId!=null)));
            CreateMap<ProfileViewModel, User>();
            CreateMap<User, ProfileViewModel>()
                .ForMember(d => d.AvatarExist, o => o.MapFrom(s => (s.AvatarId != null)));
            CreateMap<User, ChangeRoleViewModel>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.Id));
            CreateMap<News, ItemNewsListViewModel>();
            CreateMap<CreateNewsViewModel, News>();
            CreateMap<News, NewsViewModel>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Title))
                .ForMember(d => d.CategoryId, o => o.MapFrom(s => s.Category.Id))
                .ForMember(
                    p => p.ListTag,
                    opt => opt.MapFrom(x =>
                        x.NewsTag.Select(y => new TagViewModel() { TagId = y.Tag.Id, Title = y.Tag.Title }).ToList()));
            CreateMap<User, ChangeRoleViewModel>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.Id));
            CreateMap<ChatMessage, ChatMessageViewModel>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.UserName))
                .ForMember(d => d.Date, o => o.MapFrom(s => s.Date.ToString()))
                .ForMember(d => d.AvatarPath, o => o.MapFrom(s => s.User.Avatar != null ? $"/{s.User.Avatar.Path}" : "/Files/placeholder.jpg"));
            CreateMap<News, NewsAnnotationViewModel>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Title))
                .ForMember(d => d.CategoryId, o => o.MapFrom(s => s.Category.Id))
                .ForMember(d => d.CountsOfComments, o => o.MapFrom(s => s.ChatMessages.Count));

        }
    }
}
