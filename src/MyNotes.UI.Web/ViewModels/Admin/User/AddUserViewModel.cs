namespace MyNotes.UI.Web.ViewModels.Admin.User
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class AddUserViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name="First Name")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        public string Name
        {
            get
            { 
                return String.Format("{0} {1}", Firstname, Lastname); 
            }
        }

        [Display(Name = "Nick Name")]
        public string Nickname { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public Guid GroupId { get; set; }

        public string GroupName { get; set; }
    }
}