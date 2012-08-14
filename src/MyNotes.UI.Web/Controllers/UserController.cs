namespace MyNotes.UI.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.Practices.Unity;
    using MyNotes.UI.Web.GroupServiceRef;
    using MyNotes.UI.Web.UserServiceRef;
    using MyNotes.UI.Web.AccountServiceRef;
    using MyNotes.UI.Web.Setup.ActionApi;
    using MyNotes.UI.Web.Setup.Helper;
    using MyNotes.UI.Web.Setup.Extensions;
    using MyNotes.UI.Web.Setup;
    using MyNotes.UI.Web.Setup.Attributes;
    using MyNotes.UI.Web.ViewModels.Admin.Group;
    using MyNotes.UI.Web.ViewModels.Admin.User;

    public partial class UserController : MyNotesControllerBase
    {
        IServiceAction _serviceAction;
        IGroupService _groupService;
        IUserService _userService;

        public UserController(IGroupService groupService, IUserService userService)
        {
            _groupService = groupService;
            _userService = userService;
            _serviceAction = WebDependencyBuilder.Container.Resolve<IServiceAction>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("controller", this)
                                   });
        }

        [HttpGet]
        public virtual ActionResult Users()
        {
            return _serviceAction.Fetch()
                        .WithContent<IList<UserDto>, IList<UserViewModel>>(MVC.Admin.Views._users,
                                () =>
                                {
                                    var loggedInUser = Session.GetValue<LoggedUserInfoDto>(SessionKey.LoggedUser);
                                    return _userService.GetAllUsers(loggedInUser.GroupId, loggedInUser.IsSysAccount);
                                })
                        .AsJsonResult();
        }
        
        [HttpGet]
        public virtual ActionResult AddUser()
        {
            return _serviceAction.Fetch()
                        .WithPopup<SaveUserViewModel>(MVC.User.Views._addUser,
                                () =>
                                {
                                    var groups = (from gp in _groupService.GetAllGroups()
                                                    where !gp.IsSysAccount
                                                    select new SelectListItem { Value = gp.Id.ToString(), Text = gp.Name }).ToList();
                                    groups.Insert(0, new SelectListItem { Value = "", Text = "Please Select ..." });

                                    ViewData["Groups"] = groups;
                                    return new SaveUserViewModel();
                                })
                        .AsJsonResult();
        }

        [HttpPost]
        public virtual ActionResult AddUser([Bind(Exclude="Id")]SaveUserViewModel userViewModel)
        {
            return _serviceAction.Put()
                        .WithCommand(
                                () =>
                                {
                                    return _userService.AddUser(userViewModel.Firstname, userViewModel.Lastname, userViewModel.Nickname,
                                        userViewModel.Username, userViewModel.GroupId);
                                })
                        .AsJsonResult();
        }

        [HttpGet]
        public virtual ActionResult UpdateUser(Guid id)
        {
            return _serviceAction.Fetch()
                        .WithResult<UserDto, SaveUserViewModel>(MVC.User.Views._upgradeUser,
                                () =>
                                {
                                    var groups = (from gp in _groupService.GetAllGroups()
                                                  where !gp.IsSysAccount
                                                  select new SelectListItem { Value = gp.Id.ToString(), Text = gp.Name }).ToList();
                                    groups.Insert(0, new SelectListItem { Value = "", Text = "Please Select ..." });

                                    ViewData["Groups"] = groups;
                                    return _userService.GetSingleUser(id);
                                })
                        .AsJsonResult();
        }

        [HttpPost]
        public virtual ActionResult UpdateUser([Bind(Exclude="Password, ConfirmPassword")]SaveUserViewModel userViewModel)
        {
            return _serviceAction.Put()
                        .WithCommand(
                                () =>
                                {
                                    return _userService.UpdateUser(userViewModel.Id, userViewModel.Firstname, userViewModel.Lastname, 
                                        userViewModel.Nickname, userViewModel.Username, userViewModel.GroupId);
                                })
                        .AsJsonResult();
        }

        [HttpDelete]
        public virtual ActionResult DeleteUser(Guid id)
        {
            return _serviceAction.Fetch()
                        .WithResult<MyNotes.UI.Web.UserServiceRef.MessageResultDto>(
                                () =>
                                {
                                    return _userService.DeleteUser(id);
                                })
                        .AsJsonResult();
        }

        [HttpPut]
        public virtual ActionResult ResetPassword(Guid id)
        {
            return _serviceAction.Put()
                        .WithCommand(
                                () =>
                                {
                                    return _userService.ResetPassword(id);
                                })
                        .AsJsonResult();
        }

        [HttpPut]
        public virtual ActionResult UserLockStatus(Guid id, bool isLocked)
        {
            return _serviceAction.Put()
                        .WithCommand(
                                () =>
                                {
                                    return _userService.UserLockStatus(id, isLocked);
                                })
                        .AsJsonResult();
        }
    }
}
