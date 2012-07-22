namespace MyNotes.UI.Web.ViewModels.Admin.Group
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SaveGroupViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name="Group Name")]
        public string Name { get; set; }
    }
}