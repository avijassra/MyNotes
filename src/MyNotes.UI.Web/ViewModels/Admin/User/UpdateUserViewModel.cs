namespace MyNotes.UI.Web.ViewModels.Admin.User
{
    using System;

    public class UpdateUserViewModel
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Name
        {
            get
            { 
                return String.Format("{0} {1}", Firstname, Lastname); 
            }
        }
        
        public string Nickname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public Guid GroupId { get; set; }

        public string GroupName { get; set; }
    }
}