namespace MyNotes.UI.Web.ViewModels.Admin.Group
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GroupViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsSysAccount { get; set; }
    }
}