namespace MyNotes.UI.Web.ViewModels.Admin.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AccountViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid UserId { get; set; }

        public string UserNickname { get; set; }
    }
}