namespace MyNotes.Backend.Service.Contracts
{
    using System;
    using System.ServiceModel;
    using MyNotes.Backend.Dtos;
    using System.Collections.Generic;

    [ServiceContract]
    public interface IAdminService
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
        UserDto GetSingleUser(Guid id);

        [OperationContract]
        IList<UserDto> GetAllUsers(Guid groupId, bool isSysAccount);

        [OperationContract]
        MessageResultDto DeleteGroup(Guid id);
        
        [OperationContract]
        MessageResultDto AddUser(string firstname, string lastname, string nickname, string username, string password, Guid groupId);

        [OperationContract]
        MessageResultDto UpdateUser(Guid id, string firstname, string lastname, string nickname, string username, string password, Guid groupId);

        [OperationContract]
        MessageResultDto DeleteUser(Guid id);
    }
}
