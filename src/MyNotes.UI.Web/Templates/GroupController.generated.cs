// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace MyNotes.UI.Web.Controllers {
    public partial class GroupController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected GroupController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UpdateGroup() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UpdateGroup);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult DeleteGroup() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.DeleteGroup);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public GroupController Actions { get { return MVC.Group; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Group";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Group";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Index = "Index";
            public readonly string Groups = "Groups";
            public readonly string AddGroup = "AddGroup";
            public readonly string UpdateGroup = "UpdateGroup";
            public readonly string DeleteGroup = "DeleteGroup";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants {
            public const string Index = "Index";
            public const string Groups = "Groups";
            public const string AddGroup = "AddGroup";
            public const string UpdateGroup = "UpdateGroup";
            public const string DeleteGroup = "DeleteGroup";
        }


        static readonly ActionParamsClass_UpdateGroup s_params_UpdateGroup = new ActionParamsClass_UpdateGroup();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UpdateGroup UpdateGroupParams { get { return s_params_UpdateGroup; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UpdateGroup {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_DeleteGroup s_params_DeleteGroup = new ActionParamsClass_DeleteGroup();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DeleteGroup DeleteGroupParams { get { return s_params_DeleteGroup; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DeleteGroup {
            public readonly string id = "id";
        }
        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string _addGroup = "~/Views/Group/_addGroup.cshtml";
            public readonly string _upgradeGroup = "~/Views/Group/_upgradeGroup.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_GroupController: MyNotes.UI.Web.Controllers.GroupController {
        public T4MVC_GroupController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Index() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Index);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Groups() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Groups);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult AddGroup() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.AddGroup);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult AddGroup(MyNotes.UI.Web.ViewModels.Admin.Group.SaveGroupViewModel groupViewModel) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.AddGroup);
            callInfo.RouteValueDictionary.Add("groupViewModel", groupViewModel);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdateGroup(System.Guid id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdateGroup);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdateGroup(MyNotes.UI.Web.ViewModels.Admin.Group.SaveGroupViewModel groupViewModel) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdateGroup);
            callInfo.RouteValueDictionary.Add("groupViewModel", groupViewModel);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult DeleteGroup(System.Guid id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.DeleteGroup);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
