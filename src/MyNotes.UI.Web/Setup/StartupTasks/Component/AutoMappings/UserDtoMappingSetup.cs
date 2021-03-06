﻿namespace MyNotes.UI.Web.Setup.StartupTasks.Component.AutoMappings
{
    using AutoMapper;
    using MvcBase.WebHelper.StartupTasks;
    using MyNotes.UI.Web.AdminServiceRef;
    using MyNotes.UI.Web.ViewModels.Admin.User;

    public class UserDtoMappingSetup : IIncludeComponent
    {
        public void Setup()
        {
            Mapper.CreateMap<UserDto, UserViewModel>()
                .ForMember(d => d.Name, o => o.Ignore());

            Mapper.CreateMap<UserDto, SaveUserViewModel>()
                .ForMember(d => d.Name, o => o.Ignore());
        }
    }
}