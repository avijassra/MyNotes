namespace MyNotes.UI.Web.ViewModels.Admin.User
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class NewUserViewModel
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public string Nickname { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public Guid GroupId { get; set; }
    }
}