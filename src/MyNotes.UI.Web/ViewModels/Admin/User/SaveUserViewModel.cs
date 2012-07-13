namespace MyNotes.UI.Web.ViewModels.Admin.User
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SaveUserViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public string Name
        {
            get
            { 
                return String.Format("{0} {1}", Firstname, Lastname); 
            }
        }
        
        public string Nickname { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public Guid GroupId { get; set; }

        public string GroupName { get; set; }
    }
}