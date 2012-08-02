namespace MyNotes.UI.Web.ViewModels.Admin.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SaveAccountViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name="Account Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "User")]
        public Guid UserId { get; set; }
    }
}