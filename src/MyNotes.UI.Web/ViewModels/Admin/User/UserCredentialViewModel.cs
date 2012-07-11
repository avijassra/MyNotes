namespace MyNotes.UI.Web.ViewModels.Admin.User
{
    using System.ComponentModel.DataAnnotations;

    public class UserCredentialViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}