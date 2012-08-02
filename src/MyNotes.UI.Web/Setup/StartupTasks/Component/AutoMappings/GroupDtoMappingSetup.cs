namespace MyNotes.UI.Web.Setup.StartupTasks.Component.AutoMappings
{
    using AutoMapper;
    using MvcBase.WebHelper.StartupTasks;
    using MyNotes.UI.Web.GroupServiceRef;
    using MyNotes.UI.Web.ViewModels.Admin.Group;

    public class GroupDtoMappingSetup : IIncludeComponent
    {
        public void Setup()
        {
            Mapper.CreateMap<GroupDto, GroupViewModel>();

            Mapper.CreateMap<GroupDto, SaveGroupViewModel>();
        }
    }
}