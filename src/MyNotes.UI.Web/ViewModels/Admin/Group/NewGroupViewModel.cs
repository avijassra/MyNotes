namespace MyNotes.UI.Web.ViewModels.Admin.Group
{
    using System.ComponentModel.DataAnnotations;

    public class NewGroupViewModel
    {
        [Required(ErrorMessage="Name is required")]
        public string Name { get; set; }
    }
}