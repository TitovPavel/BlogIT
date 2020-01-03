using AutoMapper;
using BlogIT.DB.Models;
using BlogIT.MVC.ViewModels;
using System;

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
        }
    }
}
