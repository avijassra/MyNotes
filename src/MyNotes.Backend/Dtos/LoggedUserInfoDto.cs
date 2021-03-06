﻿namespace MyNotes.Backend.Dtos
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class LoggedUserInfoDto
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Nickname { get; set; }

        [DataMember]
        public bool FirstTimeReset { get; set; }

        [DataMember]
        public Guid GroupId { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public bool IsSysAccount { get; set; }
    }
}