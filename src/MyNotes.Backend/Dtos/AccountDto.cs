namespace MyNotes.Backend.Dtos
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class AccountDto
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Balance { get; set; }

        [DataMember]
        public virtual Guid UserId { get; set; }

        [DataMember]
        public virtual string UserNickname { get; set; }
    }
}