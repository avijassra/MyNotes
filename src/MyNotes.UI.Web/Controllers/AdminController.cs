namespace MyNotes.UI.Web.Controllers
{
    using System.Web.Mvc;
    using MyNotes.UI.Web.AdminServiceRef;
    using MyNotes.UI.Web.Setup.ActionApi;
    using MyNotes.UI.Web.Setup.Common;
    using System.Collections.Generic;
    using AutoMapper;
    using MyNotes.UI.Web.Setup.Extensions;
    using MyNotes.UI.Web.ViewModels.Admin.Group;
    using MyNotes.UI.Web.ViewModels.Admin.User;
    using MyNotes.UI.Web.UserServiceRef;
    using System.Linq;

    public partial class AdminController : Controller
    {
        IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult Groups()
        {
            return new ServiceAction(this)
                        .Fetch(SessionKey.Empty)
                        .WithContent<IList<GroupViewModel>>(MVC.Admin.Views._groups,
                                () =>
                                {
                                    var groups = _adminService.GetAllGroups();
                                    return Mapper.Map<IList<GroupViewModel>>(groups);
                                })
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult Users()
        {
            return new ServiceAction(this)
                        .Fetch(SessionKey.Empty)
                        .WithContent<IList<UserViewModel>>(MVC.Admin.Views._users,
                                () =>
                                {
                                    var loggedInUser = Session.GetValue<LoggedUserInfoDto>(SessionKey.LoggedUser);
                                    var users = _adminService.GetAllUsers(loggedInUser.GroupId, loggedInUser.IsSysAccount);
                                    return Mapper.Map<IList<UserViewModel>>(users);
                                })
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult AddGroup()
        {
            return new ServiceAction(this)
                        .Fetch(SessionKey.Empty)
                        .WithPopup<NewGroupViewModel>(MVC.Admin.Views._addGroup,
                                () =>
                                {
                                    return new NewGroupViewModel();
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult SaveGroup(NewGroupViewModel groupViewModel)
        {
            return new ServiceAction(this)
                        .Put(SessionKey.Empty)
                        .WithCommand(
                                () =>
                                {
                                    return _adminService.AddGroup(groupViewModel.Name);
                                })
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult AddUser()
        {
            return new ServiceAction(this)
                        .Fetch(SessionKey.Empty)
                        .WithPopup<NewUserViewModel>(MVC.Admin.Views._addUser,
                                () =>
                                {
                                    var groups = (from gp in _adminService.GetAllGroups()
                                                  select new SelectListItem { Value = gp.Id.ToString(), Text = gp.Name }).ToList();
                                    ViewData["Groups"] = groups;
                                    return new NewUserViewModel();
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult SaveUser(NewUserViewModel userViewModel)
        {
            return new ServiceAction(this)
                        .Put(SessionKey.Empty)
                        .WithCommand(
                                () =>
                                {
                                    return _adminService.AddUser(userViewModel.Firstname, userViewModel.Lastname, userViewModel.Nickname,
                                        userViewModel.Username, userViewModel.Password, userViewModel.GroupId);
                                })
                        .Execute();
        }
    }
}
