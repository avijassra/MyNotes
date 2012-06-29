﻿namespace MyNotes.Backend.DataAccess.DomainObjects.Entities
{
    using System;
    using System.Collections.Generic;

    internal class Group : EntityBase
    {
        public Group()
        {
            Users = new List<User>();
        }

        /// <summary>
        /// Gets or Sets the group name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or Sets the list of users in the group
        /// </summary>
        public virtual IList<User> Users { get; set; }
    }
}
