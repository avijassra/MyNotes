namespace MyNotes.Backend.Service.Contracts
{
    using System;
    using System.ServiceModel;
    using MyNotes.Backend.Dtos;
    using System.Collections.Generic;

    [ServiceContract]
    public interface IGroupService
    {
        [OperationContract]
        GroupDto GetSingleGroup(Guid id);

        [OperationContract]
        IList<GroupDto> GetAllGroups();
        
        [OperationContract]
        MessageResultDto AddGroup(string name);

        [OperationContract]
        MessageResultDto UpdateGroup(Guid id, string name);

        [OperationContract]
        MessageResultDto DeleteGroup(Guid id);
    }
}