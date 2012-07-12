namespace MyNotes.Backend.Dtos
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class MessageResultDto
    {
        [DataMember]
        public bool HasError { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public Guid Id { get; set; }

        public void ErrorMessage(string message)
        {
            HasError = true;
            Message = message;
        }

        public void SuccessMessage(string message, Guid id)
        {
            Message = message;
            Id = id;
        }
    }
}