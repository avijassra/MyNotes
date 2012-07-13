namespace MyNotes.UI.Web.ViewModels.Admin.Group
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SaveGroupViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}