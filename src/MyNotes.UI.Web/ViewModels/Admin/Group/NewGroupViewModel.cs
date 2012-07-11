namespace MyNotes.UI.Web.ViewModels.Admin.Group
{
    using System.ComponentModel.DataAnnotations;

    public class NewGroupViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}