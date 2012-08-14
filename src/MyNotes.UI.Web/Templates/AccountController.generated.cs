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
    public partial class AccountController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected AccountController(Dummy d) { }

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
        public System.Web.Mvc.ActionResult UpdateAccount() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UpdateAccount);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult DeleteAccount() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.DeleteAccount);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AccountController Actions { get { return MVC.Account; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Account";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Account";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Index = "Index";
            public readonly string Accounts = "Accounts";
            public readonly string AddAccount = "AddAccount";
            public readonly string UpdateAccount = "UpdateAccount";
            public readonly string DeleteAccount = "DeleteAccount";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants {
            public const string Index = "Index";
            public const string Accounts = "Accounts";
            public const string AddAccount = "AddAccount";
            public const string UpdateAccount = "UpdateAccount";
            public const string DeleteAccount = "DeleteAccount";
        }


        static readonly ActionParamsClass_UpdateAccount s_params_UpdateAccount = new ActionParamsClass_UpdateAccount();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UpdateAccount UpdateAccountParams { get { return s_params_UpdateAccount; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UpdateAccount {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_DeleteAccount s_params_DeleteAccount = new ActionParamsClass_DeleteAccount();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DeleteAccount DeleteAccountParams { get { return s_params_DeleteAccount; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DeleteAccount {
            public readonly string id = "id";
        }
        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string _addAccount = "~/Views/Account/_addAccount.cshtml";
            public readonly string _upgradeAccount = "~/Views/Account/_upgradeAccount.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_AccountController: MyNotes.UI.Web.Controllers.AccountController {
        public T4MVC_AccountController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Index() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Index);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Accounts() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Accounts);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult AddAccount() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.AddAccount);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult AddAccount(MyNotes.UI.Web.ViewModels.Admin.Account.SaveAccountViewModel accountViewModel) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.AddAccount);
            callInfo.RouteValueDictionary.Add("accountViewModel", accountViewModel);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdateAccount(System.Guid id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdateAccount);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdateAccount(MyNotes.UI.Web.ViewModels.Admin.Account.SaveAccountViewModel accountViewModel) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdateAccount);
            callInfo.RouteValueDictionary.Add("accountViewModel", accountViewModel);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult DeleteAccount(System.Guid id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.DeleteAccount);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
