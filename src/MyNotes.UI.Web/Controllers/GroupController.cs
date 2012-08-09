namespace MyNotes.UI.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.Practices.Unity;
    using MyNotes.UI.Web.GroupServiceRef;
    using MyNotes.UI.Web.Setup.ActionApi;
    using MyNotes.UI.Web.Setup.Helper;
    using MyNotes.UI.Web.Setup.Extensions;
    using MyNotes.UI.Web.Setup;
    using MyNotes.UI.Web.Setup.Attributes;
    using MyNotes.UI.Web.ViewModels.Admin.Group;

    public partial class GroupController : MyNotesControllerBase
    {
        IServiceAction _serviceAction;
        IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
            _serviceAction = WebDependencyBuilder.Container.Resolve<IServiceAction>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("controller", this)
                                   });
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AdminAuthorizeFilter]
        public virtual ActionResult Groups()
        {
            return _serviceAction.Fetch()
                        .WithContent<IList<GroupDto>, IList<GroupViewModel>>(MVC.Admin.Views._groups,
                                () =>
                                {
                                    return _groupService.GetAllGroups();
                                })
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult AddGroup()
        {
            return _serviceAction.Fetch()
                        .WithPopup<SaveGroupViewModel>(MVC.Group.Views._addGroup,
                                () =>
                                {
                                    return new SaveGroupViewModel();
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult AddGroup([Bind(Exclude="Id")]SaveGroupViewModel groupViewModel)
        {
            return _serviceAction.Put()
                        .WithCommand(
                                () =>
                                {
                                    return _groupService.AddGroup(groupViewModel.Name);
                                })
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult UpdateGroup(Guid id)
        {
            return _serviceAction.Fetch()
                        .WithResult<GroupDto, SaveGroupViewModel>(MVC.Group.Views._upgradeGroup,
                                () =>
                                {
                                    return _groupService.GetSingleGroup(id);
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult UpdateGroup(SaveGroupViewModel groupViewModel)
        {
            return _serviceAction.Put()
                        .WithCommand(
                                () =>
                                {
                                    return _groupService.UpdateGroup(groupViewModel.Id, groupViewModel.Name);
                                })
                        .Execute();
        }

        [HttpDelete]
        public virtual ActionResult DeleteGroup(Guid id)
        {
            return _serviceAction.Fetch()
                        .WithResult<MyNotes.UI.Web.GroupServiceRef.MessageResultDto>(
                                () =>
                                {
                                    return _groupService.DeleteGroup(id);
                                })
                        .Execute();
        }
    }
}
